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
    public class TicketFactory : GenericService<Ticket>
    {
        public readonly DateTime datenow = AppHelper.GetCurrentDate();
        private readonly UserFactory _userFactory;
        private readonly MessageQueueFactory _messageQueueFactory;

        public TicketFactory(IUnitOfWork unitOfWork, UserFactory userFactory, MessageQueueFactory messageQueueFactory) : base(unitOfWork)
        {
            _userFactory = userFactory;
            _messageQueueFactory = messageQueueFactory;
        }

        public void CreateTicket(TicketDto ticket, UserDto TicketUser, int organizationId)
        {

            this.UnitOfWork.BeginTransaction();
            try
            {

                var newTicket = new Ticket
                {
                    TicketTitle = ticket.TicketTitle,
                    CreatedById = TicketUser.UserId,
                    CreatedDate = datenow,
                    LastModifiedById = TicketUser.UserId,
                    ModifiedDate = datenow,
                    OrganizationId = organizationId,
                    FilePath = ticket.FilePath,
                    TicketDescription = ticket.TicketDescription,
                    TicketStatus = TicketStatus.New,
                    UserId = TicketUser.UserId
                };

        //        public List<UserDto> GetUserByRole(string roleName)
        //{
        //    return ExecuteProcedure<UserDto>("Sp_GetUsersByRole", roleName).ToList();
        //}

                /// Get All Admin Users and create new mail messages
                Add(newTicket);

                var adminUsers = _userFactory.GetUserByRole("Administrator", organizationId);
                var _mailContext = GetContext().Set<Mails>();
                foreach (var user in adminUsers)
                {
                    //TODO: Make Subject and message dynamic from notificcation table
                    var queue = new MessageQueue
                    {
                        NotificationType = NotificationType.NewAdminSupportTicket,
                        Comments = "New support issue: " + ticket.TicketTitle,
                        CreatedDate = datenow,
                        ModifiedDate = datenow,
                        CreatedById = TicketUser.UserId,
                        LastModifiedById = TicketUser.UserId,
                        OrganizationId = organizationId
                    };
                    _messageQueueFactory.Add(queue);
                }
                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }
    }
}
