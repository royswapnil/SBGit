using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
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
    public class BranchFactory:GenericService<Branch>
    {
        public readonly string BRANCHDDL = "branchs.ddl";
        private readonly ICacheManager _cacheManager;
        public BranchFactory( IUnitOfWork unitOfWork, ICacheManager cacheManager ) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public List<BranchDropdownListDto> BranchDDL(int organizationId)
        {
            var branches = _cacheManager.Get(BRANCHDDL, () => {

                return UnitOfWork.Repository<BranchDropdownListDto>()
                     .SqlQuery("Select Id, [Name] from Branch where OrganizationId =" + organizationId)
                     .ToList();
            });

            return branches;
        }
    }
}
