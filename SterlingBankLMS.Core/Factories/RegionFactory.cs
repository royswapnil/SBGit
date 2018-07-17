using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class RegionFactory : GenericService<Region>
    {
        public readonly string REGIONDDL = "region.ddl";
        private readonly ICacheManager _cacheManager;
        public RegionFactory( IUnitOfWork unitOfWork, ICacheManager cacheManager ) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public List<RegionDropdownListDto> RegionDDL()
        {
            var groups = _cacheManager.Get(REGIONDDL, () => {

                return UnitOfWork.Repository<RegionDropdownListDto>()
                     .SqlQuery("Select Id, Name from Region where isdeleted = 0")
                     .ToList();
            });

            return groups;
        }
    }
}
