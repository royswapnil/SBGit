using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class DepartmentFactory : GenericService<Department>
    {
        public readonly string DEPARTMENTDDL = "departments.ddl";
        private readonly ICacheManager _cacheManager;

        public DepartmentFactory(IUnitOfWork unitOfWork, ICacheManager cacheManager) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public void ClearOrganizationDepartmentDDLCache(int orgId)
        {
            var key = string.Format("{0}.{1}", DEPARTMENTDDL, orgId);
            _cacheManager.Remove(key);
        }

        public List<DepartmentDropdownListDto> GetOrganizationDepartmentDDL(int orgId)
        {

            var key = string.Format("{0}.{1}", DEPARTMENTDDL, orgId);
            var depts = _cacheManager.Get(key, () => {

                return UnitOfWork.Repository<DepartmentDropdownListDto>()
                     .SqlQuery("Select Id, Name from Department where isdeleted = 0 and organizationId=@p0", orgId)
                     .ToList();
            });

            return depts;
        }
    }
}