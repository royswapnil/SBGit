using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;

namespace SterlingBankLMS.Core.Factories
{
    public class GradeFactory : GenericService<Grade>
    {
        public GradeFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
