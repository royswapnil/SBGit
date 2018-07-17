using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SterlingBankLMS.Data.UnitofWork;

namespace SterlingBankLMS.Core.Factories
{
    public class MailFactory : GenericService<Mails>
    {
        public MailFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
