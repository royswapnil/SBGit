using Newtonsoft.Json;
using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/notification")]
    public class NotificationController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        private readonly NotificationFactory _notificationFactory;
        private readonly IPermissionService _permissionSvc;
        private readonly ICacheManager _cacheManagerSvc;
        public readonly string _notificationTagsCacheKey = "ReplaceTags-{0}";
        public readonly string _notificationGeneralTagsCacheKey = "GeneralReplaceTags";
        public readonly NotificationHubFactory _notificationHubFactory;

        public NotificationController(IWorkContext workContext, IPermissionService permissionSvc, NotificationFactory notificationFactory, ICacheManager cacheManager, NotificationHubFactory notificationHubFactory)
        {
            _workContext = workContext;
            _notificationFactory = notificationFactory;
            _permissionSvc = permissionSvc;
            _cacheManagerSvc = cacheManager;
            _notificationHubFactory = notificationHubFactory;
        }

        [HttpPost]
        [Route("SaveNotifications")]
        public IHttpActionResult SaveNotifications(List<NotificationDto> notifications)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageNotification))
                return AccessDeniedResult();

            if (!ModelState.IsValid || notifications == null || (notifications != null && notifications.Count == 0))
            {
                return BadRequest("Invalid data provided");
            }

            var result = new ApiResult<bool>();
            _notificationFactory.SaveAllNotifications(notifications, _workContext.User.OrganizationId, _workContext.User.Id);
            result.HasError = false;
            result.Message = "Your changes were successfully saved.";
            return Ok(result);
        }

        [HttpGet]
        [Route("GetNotification")]
        public IHttpActionResult GetNotification(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageNotification))
                return AccessDeniedResult();

            var result = new ApiResult<NotificationModel>();
            var notification = _notificationFactory.Find(y => y.Id == id, false);
            if (notification == null)
            {
                return BadRequest("Invalid Request! This notification does not exist.");
            }


            var notModel = notification.MapTo<Notification, NotificationModel>();

            var generalKeys = _cacheManagerSvc.Get(_notificationGeneralTagsCacheKey, () =>
            {
                return MailReplaceTagsConstants.GeneralTags;
            });


            string key = string.Format(_notificationTagsCacheKey, notModel.NotificationType.ToString());
            HashSet<NotificationReplaceTags> tags = new HashSet<NotificationReplaceTags>(generalKeys);


            var specificKeys = _cacheManagerSvc.Get(key, () =>
            {
                tags.UnionWith(MailReplaceTagsConstants.NotificationSpecificTags.Where(x => x.NotificationTypes.Any(y => y == notModel.NotificationType)));
                if (notModel.MailSetupDisabled)
                {
                    tags.RemoveWhere(x => x.Name == "IgnoreMail");
                }
                return tags;
            });

            notModel.ReplacementTags = specificKeys;
            result.HasError = false;
            result.Result = notModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateNotificationMessage")]
        public IHttpActionResult UpdateNotificationMessage(NotificationModel notification)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageNotification))
                return AccessDeniedResult();

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided");
            }
            string key = string.Format(_notificationTagsCacheKey, notification.NotificationType.ToString());
            var allowedTags = _cacheManagerSvc.Get<HashSet<NotificationReplaceTags>>(key);

            for (int i = 0; i < notification.ReplacementTags.Count; i++)
            {
                if (!allowedTags.Any(x => x.ReplaceTag == notification.ReplacementTags.ElementAt(i).ReplaceTag))
                {
                    return BadRequest("Invalid data provided");
                }
            }
            var notificationModel = notification.MapTo<NotificationModel, NotificationDto>();
            notificationModel.UsedTags = JsonConvert.SerializeObject(notification.ReplacementTags.Select(x => x.ReplaceTag).Distinct().ToArray());

            var result = new ApiResult<bool>();
            _notificationFactory. UpdateNotificationMessage(notificationModel, _workContext.User.Id);
            result.HasError = false;
            result.Message = "Your changes were successfully updated.";
            return Ok(result);
        }

        [Route("GetUserNotification")]
        public IHttpActionResult GetUserNotification(string userId )
        {
            var result = new ApiResult<List<UserNotification>>();
            var id = Convert.ToInt32(userId);
            var notifications = _notificationHubFactory.GetAllIncluding(x => x.ReceiverId == id, false).Select(x => new UserNotification
            {
                Message = x.Message,
                CreatedDate = x.CreatedDate,
                NotificationType = x.NotificationType
            }).ToList();
            if (notifications.Count > 0)
            {
                result.HasError = false;
                result.Result = notifications;
            }
            else
            {
                result.HasError = true;
            }
            return Ok(result);
        }
    }
}
