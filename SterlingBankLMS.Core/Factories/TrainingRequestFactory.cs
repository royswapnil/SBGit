using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class TrainingRequestFactory:GenericService<TrainingRequest>
    {
        public TrainingRequestFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {
        }

        public TrainingRequestDto GetTrainingRequest(int Id)
        {
            return ExecuteProcedure<TrainingRequestDto>("SP_GetTrainingRequest", Id).FirstOrDefault();
        }

        public List<TrainingRequestDto> GetUserUnapprovedTrainingRequest(int userId)
        {
            var _context = this.UnitOfWork.Repository<TrainingRequest>();
            var _trainingContext = this.UnitOfWork.Repository<Training>();
            var pendingRequests = (from a in _context.TableNoTracking
                                   join b in _trainingContext.TableNoTracking
                                   on a.TrainingId equals b.Id
                                   where !a.IsDeleted && !b.IsDeleted && a.UserId == userId && (a.TrainingApprovalStatus < TrainingApprovalStatus.Approved)
                                   select new TrainingRequestDto
                                   {
                                       Id = a.Id,
                                       TrainingId = a.TrainingId,
                                       TrainingName = b.Name,
                                       CreatedDate = a.CreatedDate,
                                       TrainingApprovalStatus = a.TrainingApprovalStatus

                                   });

            return pendingRequests.ToList();
        }

        public List<TrainingRequestDto> GetTrainingRequestPendingAdminApproval()
        {
            var _context = this.UnitOfWork.Repository<TrainingRequest>();
            var _trainingContext = this.UnitOfWork.Repository<Training>();
            var pendingRequests = (from a in _context.TableNoTracking
                                   join b in _trainingContext.TableNoTracking
                                   on a.TrainingId equals b.Id
                                   where !a.IsDeleted && !b.IsDeleted && a.TrainingApprovalStatus == TrainingApprovalStatus.PendingAdminApproval
                                   select new TrainingRequestDto
                                   {
                                       Id = a.Id,
                                       TrainingId = a.TrainingId,
                                       TrainingName = b.Name,
                                       CreatedDate = a.CreatedDate
                                   });

            return pendingRequests.ToList();
        }


        public List<TrainingRequestDto> GetTrainingRequestPendingLineManagerApproval(int lineManagerId)
        {
            return ExecuteProcedure<TrainingRequestDto>("SP_GetTrainingRequestPendingLineManagerApproval", lineManagerId).ToList();
        }

        public void ManagerApproveOrRejectTrainingRequest(MakerCheckerDto approvalAction)
        {
            var datenow = AppHelper.GetCurrentDate();
            var trainingRequestContext = UnitOfWork.Repository<TrainingRequest>();
            var request = trainingRequestContext.Get(approvalAction.Id);
            if (request == null)
            {
                throw new ArgumentNullException();
            }

            var mailContext = UnitOfWork.Repository<MessageQueue>();
            this.UnitOfWork.BeginTransaction();
            try
            {
                request.LastModifiedById = approvalAction.ActionUserId;
                request.ModifiedDate = datenow;
                var alert = new MessageQueue();

                if (approvalAction.IsApproval)
                {
                    request.TrainingApprovalStatus = TrainingApprovalStatus.PendingAdminApproval;
                    request.LineManagerActionById = approvalAction.ActionUserId;
                    request.LineManagerActionDate = datenow;
                    alert.NotificationType = NotificationType.TrainingRequestApprovedByLineManager;
                }
                else
                {
                    request.TrainingApprovalStatus = TrainingApprovalStatus.RejectedByLineManager;
                    request.LineManagerActionById = approvalAction.ActionUserId;
                    request.LineManagerActionDate = datenow;
                    alert.NotificationType = NotificationType.TrainingRequestPendingAdminApproval;
                }
                request.LineManagerComment = approvalAction.Comments;
                trainingRequestContext.Update(request);

                alert.ActionId = request.Id;
                alert.Comments = approvalAction.Comments;
                alert.CreatedById = approvalAction.ActionUserId;
                alert.CreatedDate = datenow;
                alert.LastModifiedById = approvalAction.ActionUserId;
                alert.ModifiedDate = datenow;
                alert.OrganizationId = request.OrganizationId;
                alert.ReceiverId = request.UserId;

                mailContext.Create(alert);

                this.UnitOfWork.Commit();
            }

            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void AdminApproveOrRejectTrainingRequest(MakerCheckerDto approvalAction)
        {
            var datenow = AppHelper.GetCurrentDate();
            var trainingRequestContext = UnitOfWork.Repository<TrainingRequest>();
            var userTrainingRequestContext = UnitOfWork.Repository<UserTraining>();
            var request = trainingRequestContext.Get(approvalAction.Id);
            if (request == null)
            {
                throw new ArgumentNullException();
            }

            var mailContext = UnitOfWork.Repository<MessageQueue>();
            this.UnitOfWork.BeginTransaction();
            try
            {
                request.LastModifiedById = approvalAction.ActionUserId;
                request.ModifiedDate = datenow;
                var alert = new MessageQueue();

                if (approvalAction.IsApproval)
                {
                    request.TrainingApprovalStatus = TrainingApprovalStatus.Approved;
                    request.AdminActionById = approvalAction.ActionUserId;
                    request.AdminActionDate = datenow;
                    alert.NotificationType = NotificationType.TrainingRequestApprovedByAdmin;

                    //create training if approved
                    var userTraining = new UserTraining
                    {
                        CreatedById = approvalAction.ActionUserId,
                        CreatedDate = datenow,
                        IsRequested = true,
                        LastModifiedById = approvalAction.ActionUserId,
                        ModifiedDate = datenow,
                        OrganizationId = request.OrganizationId,
                        TrainingId = request.TrainingId,
                        UserId = request.UserId
                    };
                    userTrainingRequestContext.Create(userTraining);
                }
                else
                {
                    request.TrainingApprovalStatus = TrainingApprovalStatus.RejectedByAdmin;
                    request.AdminActionById = approvalAction.ActionUserId;
                    request.AdminActionDate = datenow;
                    alert.NotificationType = NotificationType.TrainingRequestRejectedByAdmin;
                }
                request.LineManagerComment = approvalAction.Comments;
                trainingRequestContext.Update(request);

                alert.ActionId = request.Id;
                alert.Comments = approvalAction.Comments;
                alert.CreatedById = approvalAction.ActionUserId;
                alert.CreatedDate = datenow;
                alert.LastModifiedById = approvalAction.ActionUserId;
                alert.ModifiedDate = datenow;
                alert.OrganizationId = request.OrganizationId;
                alert.ReceiverId = request.UserId;

                mailContext.Create(alert);

                

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
