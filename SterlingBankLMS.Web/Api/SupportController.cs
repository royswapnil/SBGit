using DataTables.AspNet.Core;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/support")]
    public class SupportController : BaseApiController
    {

        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAcct;
        private readonly IUserRoleService _userRole;
        private readonly TicketFactory _ticketFactory;
        private readonly FileUploader _fileUploader;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageQueueFactory _messageQueueFactory;
        private readonly UserFactory _userFactory;
        private readonly MailsFactory _mailFactory;

        public SupportController(IWorkContext workContext, IUserAccountService userAcct,
            TicketFactory ticketFactory, FileUploader fileUploader,
            IUserRoleService userRole, NotificationHubFactory notificationHubFactory,
            MailsFactory mailsFactory, IUnitOfWork unitOfWork,
            MessageQueueFactory messageQueueFactory, UserFactory userFactory,
            MailsFactory mailFactory)
        {
            _workContext = workContext;
            _userAcct = userAcct;
            _ticketFactory = ticketFactory;
            _fileUploader = fileUploader;
            _userRole = userRole;
            _mailsFactory = mailsFactory;
            _notificationHubFactory = notificationHubFactory;
            _unitOfWork = unitOfWork;
            _messageQueueFactory = messageQueueFactory;
            _userFactory = userFactory;
            _mailFactory = mailFactory;
        }

        [HttpPost]
        [Route("CreateTicket")]
        public IHttpActionResult CreateTicket(TicketModel model)
        {

            if (!ModelState.IsValid) {
                return BadRequest("Please check if you have provided valid data");
            }
            var result = new ApiResult<TicketModel>();
            var newTicket = model.MapTo<TicketModel, TicketDto>();

            if (model.ImageUpload != null) {
                string pathstring = AppConstants.FileUploadPath + (int) _workContext.User.OrganizationId + "/";
                var path = _fileUploader.UploadFile(model.ImageUpload, pathstring);
                if (path != null) {
                    newTicket.FilePath = path;
                }
                else {
                    return BadRequest("An error occurred");
                }
            }

            var currentUser = _userAcct.FindUserById(_workContext.User.Id);
            var userDto = currentUser.MapTo<ApplicationUser, UserDto>();

            _ticketFactory.CreateTicket(newTicket, userDto, _workContext.User.OrganizationId);
            return Ok("Successfully Created");

        }


        [HttpGet]
        [Route("GetTicket")]
        public IHttpActionResult GetTicket(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var ticket = _ticketFactory.ExecuteProcedure<TicketDto>("SP_GetTicketList", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var obj = new SearchResponse<TicketDto>
            {
                draw = request.Draw,
                recordsTotal = ticket.Count() > 0 ? ticket[0].TotalRecords : 0,
                recordsFiltered = ticket.Count() > 0 ? ticket[0].TotalRecords : 0,
                data = ticket
            };

            return Ok(obj);
        }

        [HttpGet]
        [Route("GetTicketForTech")]
        public IHttpActionResult GetTicketTech(IDataTablesRequest request)
        {

            var ticket = _ticketFactory.ExecuteProcedure<TicketDto>("SP_GetTicketList", _workContext.User.OrganizationId, request.Search.Value, request.Start, request.Length).ToList();
            var techTickets = ticket.Where(x => x.TicketStatus == TicketStatus.Closed || x.TicketStatus == TicketStatus.Escalate).ToList();


            var obj = new SearchResponse<TicketDto>
            {
                draw = request.Draw,
                recordsTotal = techTickets.Count() > 0 ? techTickets[0].TotalRecords : 0,
                recordsFiltered = techTickets.Count(),
                data = techTickets
            };

            return Ok(obj);
        }

        [HttpGet]
        [Route("MarkTicketClosed")]
        public IHttpActionResult MarkTicketClosed(int ticketId)
        {
            var a = new ApiResult<string>();
            var result = _ticketFactory.Find(ticketId);
            if (result != null) {
                try {
                    result.LastModifiedById = _workContext.User.Id;
                    result.ModifiedDate = DateTime.Now;
                    result.TicketStatus = TicketStatus.Closed;
                    _ticketFactory.Update(result);

                    a.ErrorMessage = null;
                    a.HasError = false;
                    a.Message = "Ticket was successfully marked closed !";
                    a.Result = Enum.GetName(typeof(TicketStatus), result.TicketStatus);
                    return Ok(a);
                }
                catch (Exception) {
                    a.ErrorMessage = "Ticket was not closed. Some errors occured!";
                    a.HasError = true;
                    a.Message = "Ticket was not closed. Some errors occured!";
                    return Ok(a);
                }

            }
            else {
                a.ErrorMessage = "Ticket was not closed. Ticket Id is invalid!";
                a.HasError = true;
                a.Message = "Ticket was not closed. Ticket Id is invalid!";
                return Ok(a);
            }
        }

        [HttpGet]
        [Route("MarkTicketOpen")]
        public IHttpActionResult MarkTicketOpen(string ticketId)
        {
            var id = Convert.ToInt32(ticketId);
            var a = new ApiResult<string>();
            var result = _ticketFactory.Find(id);
            if (result != null) {
                try {
                    result.LastModifiedById = _workContext.User.Id;
                    result.ModifiedDate = DateTime.Now;
                    result.TicketStatus = TicketStatus.Open;
                    _ticketFactory.Update(result);

                    a.ErrorMessage = null;
                    a.HasError = false;
                    a.Message = "Ticket was successfully marked open!";
                    a.Result = Enum.GetName(typeof(TicketStatus), result.TicketStatus);
                    return Ok(a);
                }
                catch (Exception) {
                    a.ErrorMessage = "Ticket was not marked open. Some errors occured!";
                    a.HasError = true;
                    a.Message = "Ticket was not marked open. Some errors occured!";
                    return Ok(a);
                }

            }
            else {
                a.ErrorMessage = "Ticket was not marked open. Ticket Id is invalid!";
                a.HasError = true;
                a.Message = "Ticket was not marked open. Ticket Id is invalid!";
                return Ok(a);
            }
        }

        [Route("getTicketMessages")]
        public IHttpActionResult getTicketMessages()
        {
            var data = new ApiResult<List<TicketMessageModel>>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            if (tickets == null) {
                data.HasError = true;
            }
            else {
                data.HasError = false;
                data.Result = tickets;
            }
            return Ok(data);
        }

        [Route("getTicketMessagePaginate")]
        public IHttpActionResult getTicketMessages(int pageSize, int pageNumber)
        {
            var data = new SearchResponse<TicketMessageModel>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            if (tickets != null) {
                data.recordsTotal = tickets.Where(x => x.IsDeleted == false).Count();
                data.data = tickets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return Ok(data);
        }

        [Route("getTechTicketMessages")]
        public IHttpActionResult getTechTicketMessages()
        {
            var data = new ApiResult<List<TicketMessageModel>>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            var ticks = tickets.Where(x => x.TicketStatus == TicketStatus.Escalate || x.TicketStatus == TicketStatus.Closed || x.TicketStatus == TicketStatus.IsReadByIT).ToList();
            if (ticks == null) {
                data.HasError = true;
            }
            else {
                data.HasError = false;
                data.Result = ticks;
            }
            return Ok(data);
        }

        [Route("getSingleTicketMessages")]
        public IHttpActionResult getSingleTicketMessages(string ticketId)
        {
            var id = Convert.ToInt32(ticketId);
            var data = new ApiResult<List<TicketMessageModel>>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            var ticks = tickets.Where(x => x.TicketId == id).ToList();
            if (ticks == null) {
                data.HasError = true;
            }
            else {
                data.HasError = false;
                data.Result = ticks;
            }
            return Ok(data);
        }

        [Route("getTechTicketMessagePaginate")]
        public IHttpActionResult getTechTicketMessages(int pageSize, int pageNumber)
        {
            var data = new SearchResponse<TicketMessageModel>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            var ticks = tickets.Where(x => x.TicketStatus == TicketStatus.Escalate || x.TicketStatus == TicketStatus.Closed || x.TicketStatus == TicketStatus.IsReadByIT).ToList();
            if (ticks != null) {
                data.recordsTotal = ticks.Where(x => x.IsDeleted == false).Count();
                data.data = ticks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return Ok(data);
        }


        [Route("GetTicketDetails")]
        public IHttpActionResult GetTicketDetails(string ticketId, string status)
        {
            var id = Convert.ToInt32(ticketId);

            if (status == "New") {
                var t = _ticketFactory.GetIncluding(x => x.Id == id, true, x => x.Organization);
                if (t != null) {
                    t.TicketStatus = TicketStatus.Open;
                    t.ModifiedDate = DateTime.Now;
                    t.LastModifiedById = _workContext.User.Id;
                    _ticketFactory.Update(t);
                }
            }
            if (status == "Escalate" && _workContext.User.IsInRole("Information Technology")) {
                var t = _ticketFactory.GetIncluding(x => x.Id == id, true, x => x.Organization);
                if (t != null) {
                    t.TicketStatus = TicketStatus.IsReadByIT;
                    t.ModifiedDate = DateTime.Now;
                    t.LastModifiedById = _workContext.User.Id;
                    _ticketFactory.Update(t);
                }
            }
            var data = new ApiResult<TicketMessageModel>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            var ticket = tickets.Where(x => x.TicketId == id).FirstOrDefault();
            if (ticket != null) {
                data.HasError = false;
                data.Result = ticket;
            }
            else {
                data.HasError = true;
            }
            return Ok(data);
        }

        [Route("getTicketMessagesStatus")]
        public IHttpActionResult getTicketMessagesStatus(string status)
        {
            List<TicketMessageModel> ticket;
            var data = new ApiResult<List<TicketMessageModel>>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            if (status != "Archived") {
                ticket = tickets.Where(x => x.TicketStatusFormat == status).ToList();
                if (ticket != null) {
                    data.HasError = false;
                    data.Result = ticket;
                }
                else {
                    data.HasError = true;
                }
            }
            else {
                ticket = tickets.Where(x => x.IsDeleted == true).ToList();
                data.HasError = false;
                data.Result = ticket;
            }

            return Ok(data);
        }

        [HttpGet]
        [Route("ShowMsgReply")]
        public IHttpActionResult ShowMsgReply(string id)
        {
            var ticketId = Convert.ToInt32(id);
            var data = new ApiResult<TicketMessageModel>();
            var tickets = _ticketFactory.ExecuteProcedure<TicketMessageModel>("SP_GetTicketDetails", _workContext.User.OrganizationId).ToList();
            var ticket = tickets.Where(x => x.TicketId == ticketId).FirstOrDefault();
            if (ticket != null) {
                data.HasError = false;
                data.Result = ticket;
            }
            else {
                data.HasError = true;
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("ReplyTicket")]
        public IHttpActionResult ReplyTicket(ReplyTicketModel model)
        {
            var data = new ApiResult<string>();
            var reciver = _userAcct.FindUserByEmail(model.To);
            if (reciver != null) {
                try {

                    var mail = new Mails();
                    mail.ActionId = model.TicketId;
                    mail.CreatedDate = DateTime.Now;
                    mail.CreatedById = _workContext.User.Id;
                    mail.MailBody = model.Message;
                    mail.MailType = NotificationType.TicketReply;
                    mail.OrganizationId = _workContext.User.OrganizationId;
                    mail.MailFrom = _workContext.User.Email;
                    mail.MailFromName = _workContext.User.FirstName + "," + _workContext.User.LastName;
                    mail.MailTo = model.To;
                    mail.MailToName = reciver.FirstName + "," + reciver.LastName;
                    mail.ReceiverId = reciver.Id;
                    _mailsFactory.Add(mail);

                    data.HasError = false;
                    data.Message = "Email has been successfully batched for subsequent sending";

                }
                catch (Exception e) {

                    data.HasError = true;
                    data.Message = "" + e.Message;
                }
            }
            else {
                data.HasError = true;
                data.Message = "Staff with this email " + model.To + " does not exists";
            }


            return Ok(data);
        }


        [HttpPost]
        [Route("ForwardTicket")]
        public IHttpActionResult ForwardTicket(ReplyTicketModel model)
        {
            var data = new ApiResult<string>();
            var reciver = _userAcct.FindUserByEmail(model.To);
            if (reciver != null) {
                try {

                    var mail = new Mails();
                    mail.ActionId = model.TicketId;
                    mail.CreatedDate = DateTime.Now;
                    mail.CreatedById = _workContext.User.Id;
                    mail.MailBody = model.Message;
                    mail.MailType = NotificationType.TicketReply;
                    mail.OrganizationId = _workContext.User.OrganizationId;
                    mail.MailFrom = _workContext.User.Email;
                    mail.MailFromName = _workContext.User.FirstName + "," + _workContext.User.LastName;
                    mail.MailTo = model.To;
                    mail.MailToName = reciver.FirstName + "," + reciver.LastName;
                    mail.ReceiverId = reciver.Id;
                    _mailsFactory.Add(mail);

                    data.HasError = false;
                    data.Message = "Email has been successfully batched for subsequent sending";

                }
                catch (Exception e) {

                    data.HasError = true;
                    data.Message = "" + e.Message;
                }
            }
            else {
                data.HasError = true;
                data.Message = "Staff with this email " + model.To + " does not exists";
            }


            return Ok(data);
        }

        [HttpPost]
        [Route("DeleteBulkTicket")]
        public IHttpActionResult DeleteBulkTicket(List<TicketBulkActionModel> tickets)
        {
            var data = new ApiResult<string>();
            var tkt = tickets.Where(x => x.IsChecked == true).ToList();
            foreach (var item in tkt) {
                var ticket = _ticketFactory.GetIncluding(x => x.Id == item.Id, true, x => x.Organization);
                if (ticket != null) {
                    ticket.IsDeleted = true;
                    ticket.LastModifiedById = _workContext.User.Id;
                    ticket.ModifiedDate = DateTime.Now;

                    _ticketFactory.Update(ticket);
                }
                else {
                    data.HasError = true;
                    data.Message = "Some data are invalid";
                    return Ok(data);
                }
            }
            data.HasError = false;
            data.Message = tkt.Count() + " Tickets successfully removed";
            return Ok(data);
        }

        [HttpPost]
        [Route("CloseBulkTickets")]
        public IHttpActionResult CloseBulkTickets(List<TicketBulkActionModel> tickets)
        {
            var data = new ApiResult<string>();
            var tkts = tickets.Where(x => x.IsChecked == true).ToList();
            foreach (var item in tkts) {
                var ticket = _ticketFactory.GetIncluding(x => x.Id == item.Id, true, x => x.Organization);
                if (ticket != null) {
                    ticket.TicketStatus = TicketStatus.Closed;
                    ticket.LastModifiedById = _workContext.User.Id;
                    ticket.ModifiedDate = DateTime.Now;

                    _ticketFactory.Update(ticket);

                    var q = new MessageQueue();
                    q.ActionId = item.Id;
                    q.CreatedById = _workContext.User.Id;
                    q.CreatedDate = DateTime.Now;
                    //q.Comments = "Ticket with this title: " + ticket.TicketTitle + ", has been successfully attended to and is now closed.";
                    q.NotificationType = NotificationType.SupportTicketClosed;
                    q.OrganizationId = _workContext.User.OrganizationId;
                    q.ReceiverId = ticket.UserId;

                    _messageQueueFactory.Add(q);

                    data.HasError = false;
                    data.Message = tkts.Count() + " Tickets successfully marked closed";

                }
                else {
                    data.HasError = true;
                    data.Message = "Ticket does not exist";
                }
            }
            return Ok(data);
        }

        private async Task QueueServiceDeskEmail(Ticket supportTicket)
        {
            var user = _userFactory.Find(x => x.Id == supportTicket.UserId, false);

            ApplicationUser userAct = null;

            if (user != null) {
                userAct = await _userAcct.FindUserByIdAsync(user.ApplicationUserId);
            }

            var mail = new Mails
            {
                Subject = $"Support ticket escalated: {supportTicket.TicketTitle}",
                MailType = NotificationType.NewAdminSupportTicket,
                MailBody = supportTicket.TicketDescription,
                Attachments = supportTicket.FilePath,
                ReplyTo = userAct?.Email,
                MailTo = CommonHelper.GetAppSetting<string>(AppConstants.Keys.ServiceDeskEmailKey),
                MailFrom = CommonHelper.GetAppSetting<string>(AppConstants.Keys.AppEmailKey),
            };

            mail.CreatedById = mail.LastModifiedById = user.Id;
            mail.ModifiedDate = mail.CreatedDate = CommonHelper.GetCurrentDate();

            _mailFactory.Add(mail);
        }

        [HttpPost]
        [Route("EscalateBulkTicket")]
        public async Task<IHttpActionResult> EscalateBulkTicket(List<TicketBulkActionModel> tickets)
        {
            var data = new ApiResult<string>();
            var tkts = tickets.Where(x => x.IsChecked).ToList();
            var itUsers = _userRole.FindByName(AppConstants.Role.IT).Users.ToList();
            if (itUsers != null) {
                foreach (var item in tkts) {

                    var ticket = _ticketFactory.GetIncluding(x => x.Id == item.Id && !x.IsDeleted, true, x => x.Organization);
                    if (ticket != null) {
                        ticket.TicketStatus = TicketStatus.Escalate;
                        ticket.LastModifiedById = _workContext.User.Id;
                        ticket.ModifiedDate = CommonHelper.GetCurrentDate();

                        _ticketFactory.Update(ticket);

                        var q = new MessageQueue
                        {
                            ActionId = item.Id,
                            CreatedById = _workContext.User.Id,
                            CreatedDate = DateTime.Now,
                            Comments = "New Ticket '" + ticket.TicketTitle + "'. Kindly treat.",
                            NotificationType = NotificationType.NewITSupportTicket,
                            OrganizationId = _workContext.User.OrganizationId
                        };

                        _messageQueueFactory.Add(q);

                        data.HasError = false;
                        data.Message = tkts.Count() + " Tickets successfully escalated to the IT Department";

                        await QueueServiceDeskEmail(ticket);
                    }
                    else {
                        data.HasError = true;
                        data.Message = "Ooops! " + ticket.TicketTitle + ",  ticket seems not to exists on the platform";
                    }

                }
            }
            else {
                data.HasError = true;
                data.Message = "No IT user on this platform";
            }

            return Ok(data);
        }

        [HttpGet]
        [Route("EscalateToIt")]
        public IHttpActionResult EscalateToIt(int id)
        {
            // var ticketId = Convert.ToInt32(id);
            var data = new ApiResult<string>();
            var itUsers = _userRole.FindByName("Information Technology").Users.ToList();
            if (itUsers != null) {
                var ticket = _ticketFactory.GetIncluding(x => x.Id == id, true, x => x.Organization);
                if (ticket != null) {
                    var ticketStatus = Enum.GetName(typeof(TicketStatus), ticket.TicketStatus);
                    if (ticketStatus == "Closed") {
                        data.HasError = true;
                        data.Message = "Sorry, Ticket is already closed!";
                    }
                    else {
                        ticket.TicketStatus = TicketStatus.Escalate;
                        ticket.ModifiedDate = DateTime.Now;
                        ticket.LastModifiedById = _workContext.User.Id;

                        _ticketFactory.Update(ticket);

                        var queue = new MessageQueue();
                        queue.ActionId = id;
                        queue.CreatedById = _workContext.User.Id;
                        queue.CreatedDate = DateTime.Now;
                        queue.Comments = "Ticket with this title: " + ticket.TicketTitle + ", was escalated to IT Department by the Learning & Management. Kindly attend to it";
                        queue.NotificationType = NotificationType.NewITSupportTicket;

                        queue.OrganizationId = _workContext.User.OrganizationId;

                        _messageQueueFactory.Add(queue);

                        QueueServiceDeskEmail(ticket);

                        data.HasError = false;
                        data.Message = "Ticket successfully escalated";
                    }
                }
                else {
                    data.HasError = true;
                    data.Message = "Ooops! This ticket seems not to exists on the platform";

                }

            }
            else {
                data.HasError = true;
                data.Message = "No IT user on this platform";

            }
            return Ok(data);
        }

        [HttpGet]
        [Route("CloseSingleTicket")]
        public IHttpActionResult CloseSingleTicket(int id)
        {
            var data = new ApiResult<string>();
            var ticket = _ticketFactory.GetIncluding(x => x.Id == id, true, x => x.Organization);
            if (ticket != null) {
                ticket.TicketStatus = TicketStatus.Closed;
                ticket.ModifiedDate = DateTime.Now;
                ticket.LastModifiedById = _workContext.User.Id;

                _ticketFactory.Update(ticket);

                var queue = new MessageQueue();
                queue.ActionId = id;
                queue.CreatedById = _workContext.User.Id;
                queue.CreatedDate = DateTime.Now;
                queue.Comments = "Ticket with this title: " + ticket.TicketTitle + ", has been successfully attended to and is now closed.";
                queue.NotificationType = NotificationType.SupportTicketClosed;
                queue.OrganizationId = _workContext.User.OrganizationId;
                queue.ReceiverId = ticket.UserId;

                _messageQueueFactory.Add(queue);

                data.HasError = false;
                data.Message = "Ticket successfully closed";
            }
            else {
                data.HasError = true;
                data.Message = "Some data are invalid";
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("NewMessage")]
        public IHttpActionResult NewMessage(ReplyTicketModel model)
        {
            var data = new ApiResult<string>();
            var receiver = _userAcct.FindUserByEmail(model.To);
            if (receiver != null) {
                var mail = new Mails();
                mail.CreatedById = _workContext.User.Id;
                mail.CreatedDate = DateTime.Now;
                mail.IsSent = false;
                mail.MailBody = model.Message;
                mail.MailFrom = _workContext.User.Email;
                mail.MailFromName = _workContext.User.LastName + "," + _workContext.User.FirstName;
                mail.MailTo = model.To;
                mail.MailToName = receiver.FirstName + "," + receiver.LastName;
                mail.MailType = NotificationType.NormalMessage;
                mail.OrganizationId = _workContext.User.OrganizationId;
                mail.ReceiverId = receiver.Id;
                mail.Subject = model.Subject;

                _mailsFactory.Add(mail);


                data.HasError = false;
                data.Message = "Email successfully sent";
            }
            else {
                data.HasError = true;
                data.Message = "Sorry " + model.To + ", is invalid. No such record exists";
            }
            return Ok(data);
        }
    }
}
