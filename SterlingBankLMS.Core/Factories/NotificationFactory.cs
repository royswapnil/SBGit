using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.Factories
{
    public class NotificationFactory : GenericService<Notification>
    {
        public NotificationFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void SaveAllNotifications(List<NotificationDto> modelNotifications, int organizationId, int userId)
        {
            var savedNotifications = All(x => x.OrganizationId == organizationId, true);
            var datenow = AppHelper.GetCurrentDate();

            if (savedNotifications.Count == 0)
            {
                throw new ArgumentNullException();
            }

            this.UnitOfWork.BeginTransaction();
            try
            {
                foreach (var notification in savedNotifications)
                {
                    var modelNotification = modelNotifications.Where(x => x.NotificationType == notification.NotificationType && x.Id == notification.Id).FirstOrDefault();
                    if (modelNotification == null)
                    {
                        throw new ArgumentNullException();
                    }

                    if (notification.MailSetupDisabled)
                    {
                        notification.IsForEmployee = modelNotification.IsForEmployee;
                        notification.IsForHR = modelNotification.IsForHR;
                        notification.IsForIT = modelNotification.IsForIT;
                        notification.IsForLD = modelNotification.IsForLD;
                        notification.IsForManagement = modelNotification.IsForManagement;
                        notification.LastModifiedById = userId;
                        notification.ModifiedDate = datenow;
                    }
                    
                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void UpdateNotificationMessage(NotificationDto model, int userId)
        {
            var notification = Find(model.Id);

            if (notification == null)
            {
                throw new ArgumentNullException();
            }

            var datenow = AppHelper.GetCurrentDate();
            notification.ModifiedDate = datenow;
            notification.LastModifiedById = userId;

            if (notification.MailSetupDisabled)
            {
                model.IsNotification = notification.IsNotification;
                model.IsMail = notification.IsMail;
                model.CanIgnoreMail = notification.CanIgnoreMail;
            }

            if (model.IsNotification)
                notification.NotificationMessage = model.NotificationMessage;
            else
                notification.NotificationMessage = null;

            if (model.IsMail)
            {
                notification.MailMessage = model.MailMessage;
                notification.MailSubject = model.MailSubject;
            }
            else
            {
                notification.MailMessage = null;
                notification.MailSubject = null;
            }

            if (!notification.MailSetupDisabled)
            {
                notification.CanIgnoreMail = model.CanIgnoreMail;
                notification.IsMail = model.IsMail;
                notification.IsNotification = model.IsNotification;
            }

            Update(notification);
        }
    }
}
