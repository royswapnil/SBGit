using DataTables.AspNet.Core;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/Advert")]
    [Authorize]
    public class AdvertController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        private readonly AdvertFactory _advertFactory;
        private readonly FileUploader _fileUploader;
        private readonly IPermissionService _permissionSvc;

        public AdvertController(IWorkContext workContext, AdvertFactory advertFactory, FileUploader fileUploader, IPermissionService permissionSvc)
        {
            _workContext = workContext;
            _advertFactory = advertFactory;
            _fileUploader = fileUploader;
            _permissionSvc = permissionSvc;
        }

        [HttpGet]
        [Route("GetPagedBanner")]
        public IHttpActionResult GetPagedBanner(IDataTablesRequest request)
        {
          
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var banners = _advertFactory.GetPagedBanners(request.Search.Value, index, request.Length, _workContext.User.OrganizationId, out totalRecords).ToList();

            var bannerModel = banners.MapTo<List<AdBanner>, List<AdBannerModel>>();

            var returnObject = new SearchResponse<AdBannerModel>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = bannerModel
            };
            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetBanners")]
        public IHttpActionResult GetBanners()
        {
            var result = new ApiResult<List<AdBannerModel>>();
            var banners = _advertFactory.UnitOfWork.Repository<AdBanner>().Fetch(x => x.OrganizationId == _workContext.User.OrganizationId && !x.IsDeleted, false).ToList();
            var bannerModel = banners.MapTo<List<AdBanner>, List<AdBannerModel>>();
            result.Result = bannerModel;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBanner")]
        public IHttpActionResult GetBanner(int Id)
        {
            var result = new ApiResult<AdBannerModel>();

            var banner = _advertFactory.GetBanner(Id);
            if (banner == null)
                return BadRequest("Banner not found");

            var bannerModel = banner.MapTo<AdBanner, AdBannerModel>();

            result.HasError = false;
            result.Result = bannerModel;
            return Ok(result);
        }





        [HttpPost]
        [Route("AddorUpdateBanner")]
        public IHttpActionResult AddorUpdateBanner(AdBannerModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<AdBannerModel>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var banner = model.Id == 0 ? model.MapTo<AdBannerModel, AdBanner>() :
             _advertFactory.GetBanner(model.Id);

            if (banner == null)
            {
                return BadRequest("This banner does not exist");
            }

            if (model.ImageUpload != null && model.Id == 0)
            {
                string pathstring = AppConstants.FileUploadPath + (int)_workContext.User.OrganizationId + "/";
                var path = _fileUploader.UploadFile(model.ImageUpload, pathstring);

                if (path != null)
                {
                    banner.FileUrl = path;
                }
                else
                {
                    return BadRequest("An error occurred");
                }

            }

            var datenow = CommonHelper.GetCurrentDate();
            banner.LastModifiedById = _workContext.User.Id;
            banner.ModifiedDate = datenow;

            if (!_advertFactory.ValidateBannerNameExists(banner.Title, banner.Id))
                return BadRequest("Banner name already exists");


            if (banner.Id == 0)
            {
                banner.CreatedById = _workContext.User.Id;
                banner.OrganizationId = _workContext.User.OrganizationId;
                banner.CreatedDate = datenow;

                _advertFactory.UnitOfWork.Repository<AdBanner>().Create(banner);
            }
            else
            {
                banner.Title = model.Title;
                _advertFactory.UnitOfWork.Repository<AdBanner>().Update(banner);
            }



            var returnObj = banner.MapTo<AdBanner, AdBannerModel>();

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";

            result.Result = returnObj;
            return Ok(result);
        }



        [HttpDelete]
        [Route("DeleteBanner")]
        public IHttpActionResult DeleteBanner(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<AdBannerModel>();
            var attemptCount = _advertFactory.ValidateBannerForDelete(Id);
            if (attemptCount > 0)
                return BadRequest("This banner has has been used in " + attemptCount + " advert slots and cannot be deleted");


            var banner = _advertFactory.GetBanner(Id);
            if (banner == null)
                return BadRequest("Banner cannot be found");

            _advertFactory.DeleteBanner(banner, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Banner successfully deleted";

            return Ok(result);
        }


        [HttpGet]
        [Route("GetPagedAdvert")]
        public IHttpActionResult GetPagedAdvert(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var adverts = _advertFactory.GetPagedAdverts(request.Search.Value, index, request.Length, _workContext.User.OrganizationId, out totalRecords).ToList();
            var advertsModel = adverts.MapTo<List<AdvertDto>, List<AdvertModel>>();

            var returnObject = new SearchResponse<AdvertModel>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = advertsModel
            };
            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetAdvert")]
        public IHttpActionResult GetAdvert(int Id)
        {
           

            var result = new ApiResult<AdvertModel>();

            var advert = _advertFactory.GetIncluding(x => x.Id == Id, false, x => x.AdBanner);
            if (advert == null)
                return BadRequest("Advert not found");

            var advertModel = advert.MapTo<Advert, AdvertModel>();

            result.HasError = false;
            result.Result = advertModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult AddorUpdate(AdvertModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<AdvertModel>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var advert = model.Id == 0 ? model.MapTo<AdvertModel, Advert>() :
             _advertFactory.Find(model.Id);

            if (advert == null)
            {
                return BadRequest("This advert does not exist");
            }
            var isSpaceAvailable = _advertFactory.ValidateIfAdvertExistsForAreaandDate(advert, _workContext.User.OrganizationId);
            if (isSpaceAvailable > 0)
                return BadRequest("An advert already exists in your chosen region during this period");

            var datenow = CommonHelper.GetCurrentDate();
            advert.LastModifiedById = _workContext.User.Id;
            advert.ModifiedDate = datenow;

            if (advert.AdBannerId == null)
            {
                advert.HTMLTag = model.HTMLTag;
                advert.AdBannerId = null;
            }
            else
            {
                advert.HTMLTag = null;
                advert.AdBannerId = model.AdBannerId;
            }
            advert.ModifiedDate = datenow;
            advert.LastModifiedById = _workContext.User.Id;

            if (!_advertFactory.ValidateAdvertNameExists(advert.Title, advert.Id))
                return BadRequest("Advert name already exists");


            if (advert.Id != 0)
            {
                advert.Title = model.Title;
                advert.Section = model.Section;
                advert.AdvertLink = model.AdvertLink;
                advert.StartDate = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                advert.EndDate = DateTime.ParseExact(model.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                advert.Location = model.Location;
                _advertFactory.Update(advert);
            }
            else
            {
                advert.CreatedById = _workContext.User.Id;
                advert.CreatedDate = datenow;
                advert.OrganizationId = _workContext.User.OrganizationId;
                _advertFactory.Add(advert);
            }


            var returnObj = advert.MapTo<Advert, AdvertModel>();

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";

            result.Result = returnObj;
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteAdvert")]
        public IHttpActionResult DeleteAdvert(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<AdvertModel>();

            var advert = _advertFactory.Find(Id);
            if (advert == null)
                return BadRequest("Advert cannot be found");

            _advertFactory.DeleteAdvert(advert, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Advert successfully deleted";

            return Ok(result);
        }
    }
}
