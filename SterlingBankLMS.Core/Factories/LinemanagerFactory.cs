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
    public class LinemanagerFactory:GenericService<LineManager>
    {
        public readonly string LINEMANAGERDDL = "Linemanagers.ddl";
        private readonly ICacheManager _cacheManager;
        public LinemanagerFactory( IUnitOfWork unitOfWork, ICacheManager cacheManager ) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public List<LineManagerDropdownListDto> LinemanagerDDL()
        {
            var lines = _cacheManager.Get(LINEMANAGERDDL, () => {

                return UnitOfWork.Repository<LineManagerDropdownListDto>()
                     .SqlQuery("Select l.Id, l.LineManagerFirstName, l. LineManagerLastName ,d.[Name] as DepartmentName from LineManager l inner join Department d on d.Id= l.DepartmentId")
                     .ToList();
            });

            return lines;
        }
    }
}
