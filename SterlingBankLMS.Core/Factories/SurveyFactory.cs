using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class SurveyFactory : GenericService<Survey>
    {
        public SurveyFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool ValidateNameExists(string Name, int Id)
        {
            var existing = Find(x => x.Name == Name && x.Id != Id, false);
            return (existing == null);

        }

        public Survey FindSurvey(int surveyId, bool track = false)
        {
            return Find(x => x.Id == surveyId && !x.IsDeleted, track);
        }

        public int CountSurveyUsers(int id)
        {
            var userSurveyContext = UnitOfWork.Repository<UserSurvey>();
            var surveyContext = UnitOfWork.Repository<Survey>();

            var userSurveyCount = (from c in surveyContext.TableNoTracking
                                   join uc in userSurveyContext.TableNoTracking
                                   on c.Id equals uc.SurveyId
                                   where c.Id == id && !uc.IsDeleted
                                   select c).Count();
            return userSurveyCount;
        }

      

        /// <summary>
        /// find survey by name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<SurveyDto> SearchIndependentSurveyByName(string search)
        {
            var context = this.UnitOfWork.Repository<Survey>();

            var results = context.TableNoTracking.Where(x => x.Name.ToLower().Contains(search.ToLower()) && x.SurveyType == SurveyType.IndependentSurvey).Select(x => new SurveyDto
            {
                Id = x.Id,
                SurveyType = x.SurveyType,
                Name = x.Name,

            }).ToList();


            return results;
        }


        public IEnumerable<SurveyDto> GetSurveyPagedList(string search, int pageIndex, int pageSize, int organizationId, out int TotalRecords)
        {
            var _context = UnitOfWork.Repository<Survey>();
            var _surveyTemplateContext = UnitOfWork.Repository<SurveyTemplate>();
            var _userSurveyContext = UnitOfWork.Repository<UserSurvey>();

            var query = (from a in _context.TableNoTracking
                         join c in _surveyTemplateContext.TableNoTracking on a.TemplateId equals c.Id
                         join b in _userSurveyContext.TableNoTracking on a.Id equals b.SurveyId into j1
                         from j2 in j1.DefaultIfEmpty()

                         where a.OrganizationId == organizationId && !a.IsDeleted
                      && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && a.Name.Contains(search)))
                         group j2 by new { Name = a.Name, a.Id, a.SurveyType, a.TemplateId, Template = c.Name } into grp
                         orderby grp.Key.Id
                         select new SurveyDto { Id = grp.Key.Id, Name = grp.Key.Name, TemplateId = grp.Key.TemplateId, Template = grp.Key.Template, SurveyType = grp.Key.SurveyType, UsersTaken = grp.Count() })
                        .Skip(pageIndex * pageSize).Take(pageSize).Future();

            var queryCount = (from a in _context.TableNoTracking
                              where a.OrganizationId == organizationId && !a.IsDeleted
                              select a).DeferredCount().FutureValue();


            var surveys = query.ToList();
            TotalRecords = queryCount.Value;

            return surveys;
        }

       
    }
}
