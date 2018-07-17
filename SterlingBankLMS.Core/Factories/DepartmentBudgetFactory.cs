using Nop.Core.Caching;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;

namespace SterlingBankLMS.Core.Factories
{
    public class DepartmentBudgetFactory : GenericService<DepartmentBudget>
    {
        public readonly string DEPARTMENTDDL = "departments.ddl";
        private readonly ICacheManager _cacheManager;
        public DepartmentBudgetFactory( IUnitOfWork unitOfWork, ICacheManager cacheManager ) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }
    
        public DepartmentBudget GetDeptBudget(int deptId )
        {
            var details = GetIncluding(x => x.GroupId == deptId && x.Year == DateTime.Now.Year, false, x => x.Group);
            return details;
        }
        
    }
}
