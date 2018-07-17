﻿using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.Factories
{
    public class TrainingBudgetExpenditureFactory : GenericService<TrainingBudgetExpenditure>
    {
        public TrainingBudgetExpenditureFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {

        }
    }
}
