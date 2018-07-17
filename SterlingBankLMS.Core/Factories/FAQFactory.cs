using Nop.Core.Caching;
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
    public class FAQFactory : GenericService<FAQ>
    {
        private readonly ICacheManager _cacheManagerSvc;
        public readonly string FAQLIST = "faqList";

        public FAQFactory(IUnitOfWork unitOfWork, ICacheManager cacheManagerSvc) : base(unitOfWork)
        {
            _cacheManagerSvc = cacheManagerSvc;
        }

        public void RemoveCacheKey()
        {
            _cacheManagerSvc.Remove(FAQLIST);
        }


        public FAQ AddorUpdateFAQ(FAQ model, int userId)
        {
            var faq = model.Id == 0 ? model : Find(model.Id);
            if (faq == null)
            {
                throw new ArgumentNullException();
            }

            var datenow = AppHelper.GetCurrentDate();
            faq.ModifiedDate = datenow;
            faq.LastModifiedById = userId;

            if (model.Id != 0)
            {
                faq.Title = model.Title;
                faq.Description = model.Description;
                faq.Title = model.Title;
                Update(faq);
            }
            else
            {
                faq.SortOrder = GetFAQMaxSort() + 1;
                faq.CreatedDate = datenow;
                faq.CreatedById = userId;
                Add(faq);
            }
            _cacheManagerSvc.RemoveByPattern(FAQLIST);
            return faq;
        }

        public List<FAQ> GetAllFAQ()
        {
            string key = FAQLIST;

            return _cacheManagerSvc.Get(key,() =>
            {
                return All(x => !x.IsDeleted, false);
            });
        }

        public int GetFAQMaxSort()
        {
            var _context = UnitOfWork.Repository<FAQ>().TableNoTracking;

            var faq = _context.Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (faq == null) ? 0 : faq.SortOrder;
        }

        public bool ResortFAQ(List<FAQ> modelFAQ, int UserID)
        {

            this.UnitOfWork.BeginTransaction();
            var savedFAQ = All(x => !x.IsDeleted, true);
            try
            {
                foreach (var faq in modelFAQ)
                {
                    var savedFAQItem = savedFAQ.Where(x => x.Id == faq.Id).FirstOrDefault();
                    if (savedFAQItem != null)
                    {
                        if (faq.SortOrder != 0)
                        {
                            savedFAQItem.SortOrder = faq.SortOrder;
                            savedFAQItem.ModifiedDate = AppHelper.GetCurrentDate();
                            savedFAQItem.LastModifiedById = UserID;
                        }

                    }


                }
                this.UnitOfWork.Commit();
                _cacheManagerSvc.RemoveByPattern(FAQLIST);
                return true;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return false;
            }

        }
    }
}
