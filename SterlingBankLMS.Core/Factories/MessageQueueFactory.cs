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
    public class MessageQueueFactory : GenericService<MessageQueue>
    {
        public MessageQueueFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {

        }
    }
}
