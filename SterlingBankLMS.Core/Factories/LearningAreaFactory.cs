using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class LearningAreaFactory : GenericService<LearningArea>
    {
        private readonly ICacheManager _cacheManager;
        public readonly string LEARNINGAREADDL = "learningareaddl-{0}";

        public LearningAreaFactory(IUnitOfWork unitOfWork,ICacheManager cacheManager) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }


        public bool ValidateNameExists(string Name, int Id)
        {
            var existingArea = Find(x => x.Name == Name && x.Id != Id, false);
            return (existingArea == null);

        }


        public void RemoveLearningAreaCacheKey(int orgId)
        {
            string key = string.Format(LEARNINGAREADDL, orgId);
            _cacheManager.Remove(key);
        }

        public IEnumerable<DepartmentDropdownListDto> LearningAreaDDL(int orgId)
        {
            string key = string.Format(LEARNINGAREADDL, orgId);

            return _cacheManager.Get(key, () => {
                return All(x => !x.IsDeleted, false).Select(x => new DepartmentDropdownListDto { Id = x.Id, Name = x.Name });
            });
        }

        /// <summary>
        /// Get learning area with count of courses associated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LearningAreaDto GetLearningArea(int id)
        {
            var context = this.UnitOfWork.Repository<LearningArea>().GetContext();

            var learningArea = context.Set<LearningArea>().
                Where(x => x.IsDeleted == false && x.Id == id).Select(m => new LearningAreaDto
                {
                    LearningArea = m,
                    CourseCount = m.Courses.Count(x => !x.IsDeleted && x.LearningAreaId == id)
                }).First();

            return learningArea;
        }
        
        /// <summary>
        /// Get List of Learning Areas including count of associated courses
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>

        public List<LearningAreaDto> GetLearningAreas(int OrganizationID)
        {

            var context = this.UnitOfWork.Repository<LearningArea>().GetContext();

            var learningAreas = context.Set<LearningArea>().
                Where(x => x.IsDeleted == false).Select(m => new LearningAreaDto
                {
                    LearningArea = m,
                    CourseCount = m.Courses.Count(x => !x.IsDeleted && x.OrganizationId == OrganizationID)
                }).ToList();

            return learningAreas;
        }
    }
}
