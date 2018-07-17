using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class TrainingFactory : GenericService<Training>
    {

        public TrainingFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        //public void AddTraining(TrainingDto dto, int userId, int organizationId)
        //{
        //    var datenow = AppHelper.GetCurrentDate();
        //    var training = new Training();

        //    training.AmountPerStaff = dto.BudgetPerStaff;
        //    training.BudgetExpended = dto.Budget;
        //    training.CreatedById = userId;
        //    training.CreatedDate = datenow;
        //    training.Description = dto.Description;
        //    training.DurationInMinutes = dto.Duration;
        //    training.EndPeriod = dto.EndPeriod;
        //    training.IsDeleted = false;
        //    training.IsActive = true;
        //    training.LastModifiedById = userId;
        //    training.ModifiedDate = datenow;
        //    training.Name = dto.Name;
        //    training.OrganizationId = organizationId;
        //    training.StartPeriod = dto.StartPeriod;
        //    training.TrainingCategory = (TrainingCategory)dto.TrainingCategory;
        //    training.TrainingType = (TrainingType)dto.TrainingType;
        //    training.Vendor = dto.Vendor;
        //    training.Venue = dto.Venue;
        //    training.Year = dto.StartPeriod.Year;
        //    training.Location = dto.Location;
        //    training.PeriodFormat = JsonConvert.SerializeObject(dto.TrainingPeriod);
        //    var periods = new List<TrainingPeriod>();
        //    foreach (var period in dto.TrainingPeriod)
        //    {
        //        periods.Add(new TrainingPeriod
        //        {
        //            CreatedById = userId,
        //            CreatedDate = datenow,
        //            Day = period.Day,
        //            StartTime = period.StartTime,
        //            EndTime = period.EndTime,
        //            EndSpan = period.EndSpan,
        //            StartSpan = period.StartSpan,
        //            LastModifiedById = userId,
        //            ModifiedDate = datenow,
        //            OrganizationId = organizationId
        //        });
        //    };

        //    training.TrainingPeriod = periods;

        //    Add(training);
        //}

        public void AddorUpdateTraining(Training training, TrainingDto trainingModel, int userId, int organizationId)
        {
            var datenow = AppHelper.GetCurrentDate();
            this.UnitOfWork.BeginTransaction();
            try
            {
                if (training.Id == 0)
                {
                    training.LastModifiedById = userId;
                    training.ModifiedDate = datenow;
                    training.CreatedById = userId;
                    training.CreatedDate = datenow;
                    training.OrganizationId = organizationId;
                    var periods = new List<TrainingPeriod>();
                    foreach (var period in trainingModel.TrainingPeriod)
                    {
                        periods.Add(new TrainingPeriod
                        {
                            CreatedById = userId,
                            CreatedDate = datenow,
                            Day = period.Day,
                            StartTime = period.StartTime,
                            EndTime = period.EndTime,
                            EndSpan = period.EndSpan,
                            StartSpan = period.StartSpan,
                            LastModifiedById = userId,
                            ModifiedDate = datenow,
                            OrganizationId = organizationId
                        });
                    };

                    training.TrainingPeriod = periods;
                    training.PeriodFormat = JsonConvert.SerializeObject(trainingModel.TrainingPeriod);
                    Add(training);
                }
                else
                {
                    training.AmountPerStaff = trainingModel.BudgetPerStaff;
                    training.BudgetExpended = trainingModel.Budget;

                    training.Description = trainingModel.Description;
                    training.DurationInMinutes = trainingModel.Duration;
                    training.EndPeriod = trainingModel.EndPeriod;

                    training.Name = trainingModel.Name;
                    training.OrganizationId = organizationId;
                    training.StartPeriod = trainingModel.StartPeriod;
                    training.TrainingCategory = (TrainingCategory)trainingModel.TrainingCategory;
                    training.TrainingType = (TrainingType)trainingModel.TrainingType;
                    training.Vendor = trainingModel.Vendor;
                    training.Venue = trainingModel.Venue;
                    training.Year = (trainingModel.StartPeriod == null) ? null : (int?)trainingModel.StartPeriod.Value.Year;
                    training.Location = trainingModel.Location;
                    training.PeriodFormat = JsonConvert.SerializeObject(trainingModel.TrainingPeriod);

                    foreach (var item in trainingModel.TrainingPeriod)
                    {
                        if (item.Id == 0)
                        {
                            training.TrainingPeriod.Add(new TrainingPeriod
                            {
                                CreatedById = userId,
                                CreatedDate = datenow,
                                Day = item.Day,
                                StartTime = item.StartTime,
                                EndTime = item.EndTime,
                                EndSpan = item.EndSpan,
                                StartSpan = item.StartSpan,
                                LastModifiedById = userId,
                                ModifiedDate = datenow,
                                OrganizationId = organizationId
                            });
                        }
                        else
                        {
                            var oldTrain = training.TrainingPeriod.Where(x => x.Id == item.Id).FirstOrDefault();

                            oldTrain.Day = item.Day;
                            oldTrain.StartTime = item.StartTime;
                            oldTrain.EndTime = item.EndTime;
                            oldTrain.EndSpan = item.EndSpan;
                            oldTrain.StartSpan = item.StartSpan;
                            oldTrain.LastModifiedById = userId;
                            oldTrain.ModifiedDate = datenow;

                        }



                    }
                    Update(training);
                }
                this.UnitOfWork.Commit();
            }

            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }


        }


        public void AddTrainingRequest(TrainingRequest trainingRequest, int userId, int? lineManagerId, int organizationId)
        {
            var datenow = AppHelper.GetCurrentDate();
            var trainingRequestContext = UnitOfWork.Repository<TrainingRequest>();
            var mailContext = UnitOfWork.Repository<MessageQueue>();
            this.UnitOfWork.BeginTransaction();
            try
            {
                trainingRequest.LastModifiedById = userId;
                trainingRequest.ModifiedDate = datenow;
                trainingRequest.CreatedById = userId;
                trainingRequest.CreatedDate = datenow;
                trainingRequest.OrganizationId = organizationId;
                trainingRequest.TrainingApprovalStatus = TrainingApprovalStatus.PendingLineManagerApproval;
                trainingRequest.UserId = userId;

                trainingRequestContext.Create(trainingRequest);

                var mail = new MessageQueue
                {
                    ActionId = trainingRequest.Id,
                    Comments = trainingRequest.ReasonForRequest,
                    NotificationType = NotificationType.NewTrainingRequest,
                    CreatedById = userId,
                    CreatedDate = AppHelper.GetCurrentDate(),
                    LastModifiedById = userId,
                    ModifiedDate = AppHelper.GetCurrentDate(),
                    OrganizationId = organizationId,
                    ReceiverId = lineManagerId.Value
                };
                mailContext.Create(mail);

                this.UnitOfWork.Commit();
            }

            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }


        }


        public bool DeleteTrainingPeriod(Training training, List<TrainingPeriodDto> periods, int periodId, int userId)
        {
            var _trainingPeriodContext = UnitOfWork.Repository<TrainingPeriod>().TableNoTracking;

            this.UnitOfWork.BeginTransaction();
            try
            {
                var period = training.TrainingPeriod.Where(x => x.Id == periodId).FirstOrDefault();
                if (period == null)
                    throw new ArgumentNullException();

                period.IsDeleted = true;
                period.LastModifiedById = userId;
                period.ModifiedDate = AppHelper.GetCurrentDate();
                periods.RemoveAll(x => x.Id == periodId);
                training.PeriodFormat = JsonConvert.SerializeObject(periods);


                this.UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return false;
            }


        }

        public Training GetTraining(int Id, bool track)
        {
            var training = GetIncluding(x => x.Id == Id, track, x => x.TrainingPeriod.Where(y => !y.IsDeleted));
            return training;
        }

        public List<TrainingDto> GetPagedTraining(int organizationId, int pageSize, int pageNumber, string search, out int TotalRecords)
        {

            var _trainingContext = UnitOfWork.Repository<Training>().TableNoTracking;



            var query = (from a in _trainingContext.Include(x => x.TrainingPeriod)

                         where a.OrganizationId == organizationId && !a.IsDeleted
                      && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && a.Name.Contains(search)))
                         orderby a.Id
                         select new TrainingDto
                         {
                             Id = a.Id,
                             Description = a.Description,
                             Duration = a.DurationInMinutes,
                             StartPeriod = a.StartPeriod,
                             EndPeriod = a.EndPeriod,
                             Location = a.Location,
                             Name = a.Name,
                             TrainingCategory = (int)a.TrainingCategory,
                             TrainingType = (int)a.TrainingType,
                             Vendor = a.Vendor,
                             Venue = a.Venue,
                             PeriodFormat = a.PeriodFormat
                         })
                        .Skip(pageNumber * pageSize).Take(pageSize).Future();


            var queryCount = (from a in _trainingContext
                              where a.OrganizationId == organizationId && !a.IsDeleted
                              select a).DeferredCount().FutureValue();

            TotalRecords = queryCount.Value;

            var ReturnList = query.ToList();

            return ReturnList;



        }

      

        public DepartmentBudgetDto GetBudgetToUpdate(int Id, int organId)
        {
            var context = UnitOfWork.Repository<DepartmentBudget>().GetContext();
            var budget = context.Set<DepartmentBudget>().Where(x => x.Id == Id && x.IsDeleted == false && x.OrganizationId == organId).FirstOrDefault();
            if (budget != null)
            {
                var dto = new DepartmentBudgetDto()
                {
                    Budget = budget.Budget,
                    AmountSpent = budget.AmountSpent,
                    GroupId = budget.GroupId,
                    Id = budget.Id,
                    RegionId = budget.RegionId,
                    GroupName = budget.Group.Name,
                    RegionName = budget.Region.Name,
                    Year = budget.Year,

                };
                return dto;
            }
            return null;
        }

        //public void UpdateTraining(TrainingDto dto)
        //{
        //    var context = this.UnitOfWork.Repository<Training>().GetContext();
        //    var training = context.Set<Training>().Where(x => x.Id == dto.Id && x.IsDeleted == false).FirstOrDefault();
        //    if (training != null)
        //    {

        //        training.AmountPerStaff = dto.BudgetPerStaff;
        //        training.BudgetExpended = dto.Budget;
        //        training.DurationInMinutes = dto.Duration;
        //        training.EndPeriod = dto.EndPeriod;
        //        training.Name = dto.Name;
        //        training.StartPeriod = dto.StartPeriod;
        //        training.TrainingCategory = (TrainingCategory)dto.TrainingCategory;
        //        training.TrainingType = (TrainingType)dto.TrainingType;
        //        training.Vendor = dto.Vendor;
        //        training.Venue = dto.Venue;
        //        training.Year = dto.StartPeriod.Year;
        //        training.Location = dto.Location;


        //        context.SaveChanges();

        //    }
        //}

        public void UpdateBudget(DepartmentBudgetDto dto)
        {
            var context = this.UnitOfWork.Repository<DepartmentBudget>().GetContext();
            var budget = context.Set<DepartmentBudget>().Where(x => x.Id == dto.Id && x.IsDeleted == false).FirstOrDefault();
            if (budget != null)
            {

                budget.Budget = dto.Budget;
                budget.GroupId = dto.GroupId;
                budget.RegionId = dto.RegionId;
                budget.Year = dto.Year;

                context.SaveChanges();

            }
        }

        public List<AssignedTrainingModel> PopulateTrainingName(int organizationId)
        {
            var trainings = ExecuteProcedure<TrainingDto>("SP_GetTrainingNames", organizationId).ToList();
            var viewModel = new List<AssignedTrainingModel>();

            foreach (var training in trainings)
            {
                viewModel.Add(new AssignedTrainingModel
                {
                    TrainingId = training.Id,
                    TrainingName = training.Name
                });
            }
            return viewModel;
        }

        public void AddTrainingBudget(DepartmentBudgetDto dto, int userId, int organId)
        {
            var context = this.UnitOfWork.Repository<DepartmentBudget>().GetContext();
            var budget = new DepartmentBudget();

            budget.AmountSpent = 0m;
            budget.Budget = dto.Budget;
            budget.CreatedById = userId;
            budget.CreatedDate = DateTime.Now;
            budget.GroupId = dto.GroupId;
            budget.RegionId = dto.RegionId;
            budget.IsActive = true;
            budget.IsDeleted = false;
            budget.LastModifiedById = userId;
            budget.OrganizationId = organId;
            budget.Year = DateTime.Now.Year;

            context.Set<DepartmentBudget>().Add(budget);
            context.SaveChanges();
        }

        public Training GetTrainingDetails(int trainingId)
        {
            var training = Find(trainingId);
            return training;
        }


        public List<Training> GetTrainingByPeriod(DateTime startDate, DateTime endDate, int organizationId)
        {
            var training = GetAllIncluding(x => x.OrganizationId == organizationId && !x.IsDeleted

            && x.EndPeriod >= startDate && x.StartPeriod <= endDate, false, x => x.TrainingPeriod.Where(y => !y.IsDeleted)
            );

            return training.ToList();
        }

        public List<Training> GetUserTrainingByPeriod(DateTime startDate, DateTime endDate, int userId)
        {
            var _context = this.UnitOfWork.Repository<UserTraining>();
            var _trainingContext = this.UnitOfWork.Repository<Training>();

            var userTrainings = (from a in _context.TableNoTracking
                                 join b in _trainingContext.TableNoTracking
                                 on a.TrainingId equals b.Id
                                 where !a.IsDeleted && !b.IsDeleted && a.UserId == userId && b.EndPeriod >= startDate && b.StartPeriod <= endDate
                                 select b);
            return userTrainings.ToList();
        }

        public List<TrainingCalendarDto> GetUserOrgTrainingByMonth(DateTime? startDate, DateTime? endDate, int userId, int organizationId, int? pageSize, int? pageNo)
        {
            return ExecuteProcedure<TrainingCalendarDto>("SP_GetUserOrgTrainingByMonth", userId,organizationId, startDate, endDate,pageSize,pageNo).ToList();

            //var trainings = (from a in _context.TableNoTracking.Include(x => x.TrainingPeriod)
            //                     join b in _trainingContext.TableNoTracking
            //                     on new {Key1= a.Id, Key2 = true, Key3 = true} 
            //                     equals new {Key1 = b.TrainingId,Key2 = b.UserId == userId , Key3 = !b.IsDeleted} into j1
            //                     from j2 in j1.DefaultIfEmpty()
            //                     join c in _trainingReqContext.TableNoTracking
            //                      on new { Key1 = a.Id, Key2 = true, Key3 = true,Key4 = true } 
            //                      equals new { Key1 = c.TrainingId, Key2 = c.UserId == userId,
            //                          Key3 = !c.IsDeleted,Key4= c.TrainingApprovalStatus == TrainingApprovalStatus.Pending } into j3
            //                     from j4 in j3.DefaultIfEmpty()

            //                     where !a.IsDeleted && !j4.IsDeleted && a.EndPeriod >= startDate && a.StartPeriod <= endDate
            //                     select new TrainingCalendarDto {
            //                         EndPeriod = a.EndPeriod,
            //                         Id = a.Id,
            //                         Description = a.Description,
            //                         Location = a.Location,
            //                         Name = a.Name,
            //                         StartPeriod = a.StartPeriod,
            //                         TrainingCategory = a.TrainingCategory,
            //                         TrainingType = a.TrainingType,
            //                         Vendor = a.Vendor,
            //                         Venue = a.Venue,
            //                         TrainingPeriod = a.TrainingPeriod.Select(x=> new TrainingPeriodDto { Day = x.Day,EndTime = x.EndTime,Id = x.Id,StartTime = x.StartTime })
            //                     });
            //return trainings.ToList();
        }

        /// <summary>
        /// find training by name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<TrainingDto> SearchTrainingByName(string search)
        {
            var context = this.UnitOfWork.Repository<Training>();

            var results = context.TableNoTracking.Where(x => x.Name.ToLower().Contains(search.ToLower())).Select(x => new TrainingDto
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();


            return results;
        }

        public List<Training> GetUpcomingTrainings(int UserId, int Count)
        {
            var _context = this.UnitOfWork.Repository<UserTraining>();
            var _trainingContext = this.UnitOfWork.Repository<Training>();
            var datenow = AppHelper.GetCurrentDate();

            var userTrainings = (from a in _context.TableNoTracking
                                 join b in _trainingContext.TableNoTracking
                                 on a.TrainingId equals b.Id
                                 where !a.IsDeleted && !b.IsDeleted && a.UserId == UserId && b.EndPeriod >= datenow && b.StartPeriod != null && b.EndPeriod != null
                                 orderby b.StartPeriod

                                 select b);
            return userTrainings.Take(Count).ToList();
        }

     
    }
}
