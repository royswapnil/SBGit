using SterlingBankLMS.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Data.Models.Entities;

namespace SterlingBankLMS.Core.Factories
{
    public class SurveyResponseFactory : GenericService<SurveyQuestionResponse>
    {
        public SurveyResponseFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
