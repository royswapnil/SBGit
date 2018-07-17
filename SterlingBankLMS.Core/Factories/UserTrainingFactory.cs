using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.Factories
{
    public class UserTrainingFactory : GenericService<UserTraining>
    {
        public UserTrainingFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {
        }
        
    }
}


