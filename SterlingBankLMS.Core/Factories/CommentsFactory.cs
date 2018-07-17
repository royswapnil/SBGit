using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;

namespace SterlingBankLMS.Core.Factories
{
    public class CommentsFactory : GenericService<Comments>
    {
        public readonly DateTime datenow = AppHelper.GetCurrentDate();
        private readonly UserFactory _userFactory;
        private readonly MessageQueueFactory _messageQueueFactory;

        public CommentsFactory(IUnitOfWork unitOfWork, UserFactory userFactory, MessageQueueFactory messageQueueFactory) : base(unitOfWork)
        {
            _userFactory = userFactory;
            _messageQueueFactory = messageQueueFactory;
        }

    }
}
