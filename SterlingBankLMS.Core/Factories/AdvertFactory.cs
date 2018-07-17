using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class AdvertFactory : GenericService<Advert>
    {
        public AdvertFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AdBanner GetBanner(int Id)
        {
            var _context = UnitOfWork.Repository<AdBanner>();
            return _context.Get(Id);
        }

        public bool ValidateBannerNameExists(string Title, int Id)
        {
            var _context = this.UnitOfWork.Repository<AdBanner>();
            var existing = _context.Get(x => x.Title == Title && x.Id != Id, false);
            return (existing == null);

        }

        public bool ValidateAdvertNameExists(string Title, int Id)
        {
            var existing = Find(x => x.Title == Title && x.Id != Id, false);
            return (existing == null);

        }

        public IEnumerable<AdBanner> GetPagedBanners(string search, int pageIndex, int pageSize, int organizationId, out int TotalRecords)
        {

            var _context = UnitOfWork.Repository<AdBanner>();

            var query = (from a in _context.TableNoTracking
                         where !a.IsDeleted && a.OrganizationId == organizationId
                         && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && a.Title.Contains(search)))
                         select a)
                             .OrderBy(x => x.Id)
                            .Skip(pageIndex * pageSize).Take(pageSize).Future();

            var queryCount = (from a in _context.TableNoTracking
                              where a.OrganizationId == organizationId && !a.IsDeleted
                              select a).DeferredCount().FutureValue();


            var banner = query.ToList();
            TotalRecords = queryCount.Value;

            return banner;
        }

        public AdvertDto GetCurrentSideLocationBanner(int location)
        {
            var datenow = AppHelper.GetCurrentDate();
            var advert = this.GetAllIncluding(a => !a.IsDeleted 
            && (a.Location == (AdvertLocation)location || a.Location == AdvertLocation.All) && a.Section == AdvertSections.SideContent
            && a.StartDate <= datenow && a.EndDate >= datenow, false, x => x.AdBanner);

            return advert.Select(x => new AdvertDto
            {
                AdBannerId = x.AdBannerId,
                AdvertLink = x.AdvertLink,
                FileUrl = x.AdBanner.FileUrl,
                HTMLTag = x.HTMLTag,
                Title = x.Title
            }).FirstOrDefault();
        }

        public AdvertDto GetCurrentTopLocationBanner(int location)
        {
            var datenow = AppHelper.GetCurrentDate();
            var advert = this.GetAllIncluding(a => !a.IsDeleted
            && (a.Location == (AdvertLocation)location || a.Location == AdvertLocation.All) && a.Section == AdvertSections.TopBanner
            && a.StartDate <= datenow && a.EndDate >= datenow && a.AdBannerId != null, false, x => x.AdBanner);

           return advert.Select(x => new AdvertDto {
                AdBannerId = x.AdBannerId,
                AdvertLink = x.AdvertLink,
                FileUrl = x.AdBanner.FileUrl,
                HTMLTag = x.HTMLTag,
                Title = x.Title
            }).FirstOrDefault();
        
        }

        public int ValidateIfAdvertExistsForAreaandDate(Advert Advert,  int organizationId)
        {
          
            var advertContext = UnitOfWork.Repository<Advert>();

            var advertCount = (from c in advertContext.TableNoTracking
                               where c.Id != Advert.Id && !c.IsDeleted && c.OrganizationId == organizationId
                               && c.StartDate < Advert.EndDate && c.EndDate > Advert.EndDate && Advert.Location == c.Location && Advert.Section == c.Section
                               select c).Count();
            return advertCount;
        }


        public int ValidateBannerForDelete(int id)
        {
            var bannerContext = UnitOfWork.Repository<AdBanner>();
            var advertContext = UnitOfWork.Repository<Advert>();

            var surveyCount = (from c in bannerContext.TableNoTracking
                               join uc in advertContext.TableNoTracking
                               on c.Id equals uc.AdBannerId
                               where c.Id == id && !uc.IsDeleted
                               select c).Count();
            return surveyCount;
        }


        public IEnumerable<AdvertDto> GetPagedAdverts(string search, int pageIndex, int pageSize, int organizationId, out int TotalRecords)
        {

            var _context = UnitOfWork.Repository<Advert>();
            var _advertBannercontext = UnitOfWork.Repository<AdBanner>();

            var query = (from a in _context.TableNoTracking.Include(x => x.AdBanner)
                         where !a.IsDeleted && a.OrganizationId == organizationId
                         && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && a.Title.Contains(search)))
                         select new AdvertDto
                         {
                             Id = a.Id,
                             AdvertLink = a.AdvertLink,
                             Title = a.Title,
                             StartDate = a.StartDate,
                             EndDate = a.EndDate,
                             HTMLTag = a.HTMLTag,
                             IsActive = a.IsActive,
                             AdBannerId = a.AdBannerId,
                             Section = a.Section,
                             Location = a.Location,
                             FileUrl = a.AdBanner == null  ? null : a.AdBanner.FileUrl
                         })
                             .OrderBy(x => x.Id)
                            .Skip(pageIndex * pageSize).Take(pageSize).Future();

            var queryCount = (from a in _context.TableNoTracking
                              where a.OrganizationId == organizationId && !a.IsDeleted
                              select a).DeferredCount().FutureValue();


            var adverts = query.ToList();
            TotalRecords = queryCount.Value;

            return adverts;
        }


        public void DeleteBanner(AdBanner banner, int userId)
        {
            var _context = UnitOfWork.Repository<AdBanner>();
            banner.IsDeleted = true;
            banner.LastModifiedById = userId;
            banner.ModifiedDate = AppHelper.GetCurrentDate();
            _context.Update(banner);
        }


        public void DeleteAdvert(Advert advert, int userId)
        {
            advert.IsDeleted = true;
            advert.LastModifiedById = userId;
            advert.ModifiedDate = AppHelper.GetCurrentDate();
            Update(advert);
        }

        public List<Advert> GetAllAdvert()
        {
            var context = this.UnitOfWork.Repository<Advert>().GetContext();
            var result = new List<Advert>();
            var actual = context.Set<Advert>().ToList();

            foreach (var n in actual)
            {
                var organization = GetContext().Set<Organization>().Find(n.OrganizationId);
                result.Add(new Advert()
                {
                    Organization = organization,
                    AdvertLink = n.AdvertLink,
                    //Description = n.Description,
                    EndDate = n.EndDate,
                    //FileUrl = n.FileUrl,
                    IsActive = n.IsActive,
                    IsDeleted = n.IsDeleted,
                    Section = n.Section,
                    StartDate = n.StartDate
                });
            }
            return result;
        }

        public bool Add_Advert(AdvertDto add_ad)
        {
            this.UnitOfWork.Repository<Advert>().GetContext();
            try
            {
                Advert newAd = new Advert()
                {
                    //AdvertLink = add_ad.URL,
                    // Description = add_ad.Description,
                    //FileUrl = add_ad.FileUrl,
                    Section = (AdvertSections)add_ad.Section,
                    StartDate = add_ad.StartDate,
                    EndDate = add_ad.EndDate,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    OrganizationId = 1
                };
                this.Add(newAd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
