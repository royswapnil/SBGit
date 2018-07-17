using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class GroupFactory : GenericService<Group>
    {
        public readonly string GROUPDDL = "groups.ddl";
        private readonly ICacheManager _cacheManager;

        public void ClearOrganizationGroupDDLCache(int orgId)
        {
            var key = string.Format("{0}.{1}", GROUPDDL, orgId);
            _cacheManager.Remove(key);
        }

        public GroupFactory(IUnitOfWork unitOfWork, ICacheManager cacheManager) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public List<GroupDropdownListDto> GroupOrganizationDDL(int orgId)
        {

            var key = string.Format("{0}.{1}", GROUPDDL, orgId);
            var groups = _cacheManager.Get(key, () => {
                return UnitOfWork.Repository<GroupDropdownListDto>()
                     .SqlQuery("Select Id, Name from [Group] where isdeleted = 0 and organizationId=@p0 ", orgId)
                     .ToList();
            });

            return groups;
        }
    }
}