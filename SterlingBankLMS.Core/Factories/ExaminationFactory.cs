using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SterlingBankLMS.Core.Factories
{
    public class ExaminationFactory : GenericService<Examination>
    {
        public ExaminationFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool ValidateExamination(int id, int organizationId)
        {
            return ExecuteProcedure<UserExamDto>("Sp_validateOrganizationExamination", id, organizationId).FirstOrDefault() != null;
        }

        public bool ValidateNameExists(string Name, int Id)
        {
            var existing = Find(x => x.Name == Name && x.Id != Id, false);
            return (existing == null);
        }

        public ExaminationQuestion AddorUpdateExamQuestion(ExaminationQuestion modelQuestion, int UserID, int OrgId)
        {
            var examQuestion = new ExaminationQuestion();
            if (modelQuestion.Id > 0) {
                examQuestion = this.GetExamQuestionIncludingOptions(modelQuestion.Id);
            }

            var datenow = AppHelper.GetCurrentDate();


            if (examQuestion == null)
                return null;

            this.UnitOfWork.BeginTransaction();
            try {
                if (modelQuestion.Id == 0) {
                    //examQuestion.OrganizationId = OrgId;
                    examQuestion.CreatedById = UserID;
                    examQuestion.CreatedDate = datenow;
                    examQuestion.SortOrder = GetExamQuestionMaxSort(modelQuestion.ExaminationId) + 1;

                    var optionList = new List<ExaminationQuestionOption>();
                    foreach (var option in modelQuestion.Options) {
                        option.CreatedById = UserID;
                        option.LastModifiedById = UserID;
                        option.ModifiedDate = AppHelper.GetCurrentDate();
                        option.CreatedDate = AppHelper.GetCurrentDate();
                        //option.OrganizationId = OrgId;
                        optionList.Add(option);
                    }
                    examQuestion.Options = optionList;
                }

                examQuestion.AnswerType = modelQuestion.AnswerType;
                examQuestion.ExaminationId = modelQuestion.ExaminationId;
                examQuestion.IsMultipleChoice = modelQuestion.IsMultipleChoice;
                examQuestion.Question = modelQuestion.Question;
                examQuestion.Weight = modelQuestion.Weight;


                examQuestion.LastModifiedById = UserID;
                examQuestion.ModifiedDate = datenow;


                if (modelQuestion.Id > 0) {
                    foreach (var option in modelQuestion.Options) {
                        if (option.Id == 0) {
                            option.CreatedById = UserID;
                            option.LastModifiedById = UserID;
                            option.ModifiedDate = AppHelper.GetCurrentDate();
                            option.CreatedDate = AppHelper.GetCurrentDate();
                            //option.OrganizationId = OrgId;
                            examQuestion.Options.Add(option);
                        }
                        else {
                            var examOption = examQuestion.Options.Where(x => x.Id == option.Id).FirstOrDefault();
                            examOption.Title = option.Title;
                            examOption.Value = option.Value;
                            examOption.IsAnswer = option.IsAnswer;
                            examOption.LastModifiedById = UserID;
                            examOption.ModifiedDate = datenow;
                        }

                    }
                }
                if (examQuestion.Id == 0) {
                    GetContext().Set<ExaminationQuestion>().Add(examQuestion);
                }

                this.UnitOfWork.Commit();

                return examQuestion;
            }


            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                return null;
            }
        }



        public ExaminationQuestion GetExamQuestionIncludingOptions(int Id)
        {
            var examQuestionContext = UnitOfWork.Repository<ExaminationQuestion>();
            var questionOptionContext = GetContext().Set<ExaminationQuestionOption>();

            var examQuestion = examQuestionContext.GetAllIncluding(x => x.Id == Id, true, x => x.Options).FirstOrDefault();

            return examQuestion;
        }

        public void DeleteExam(Examination entity, int userID)
        {
            this.UnitOfWork.BeginTransaction();
            try {

                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                foreach (var question in entity.ExamQuestions) {
                    question.IsDeleted = true;
                    question.LastModifiedById = userID;
                    question.ModifiedDate = AppHelper.GetCurrentDate();

                    foreach (var options in question.Options) {
                        options.IsDeleted = true;
                        options.LastModifiedById = userID;
                        options.ModifiedDate = AppHelper.GetCurrentDate();
                    }
                }


                this.UnitOfWork.Commit();
            }
            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void DeleteExamQuestion(ExaminationQuestion entity, int userID)
        {
            this.UnitOfWork.BeginTransaction();
            try {
                var _context = GetContext().Set<ExaminationQuestion>();
                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                foreach (var option in entity.Options) {
                    option.IsDeleted = true;
                    option.LastModifiedById = userID;
                    option.ModifiedDate = AppHelper.GetCurrentDate();
                }

                GetContext().SaveChanges();
                this.UnitOfWork.Commit();
            }
            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void DeleteQuestionOption(ExaminationQuestionOption entity, int userID)
        {

            try {
                var _context = GetContext().Set<ExaminationQuestionOption>();
                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                GetContext().SaveChanges();
            }
            catch (Exception ex) {

                throw;
            }

        }

        public int ValidateExamForDelete(int id)
        {
            var examContext = GetContext().Set<Examination>();
            var examAttemptContext = GetContext().Set<ExaminationAttempt>();

            var examAttemptCount = (from c in examContext
                                    join uc in examAttemptContext
                                    on c.Id equals uc.ExaminationId
                                    where c.Id == id && !uc.IsDeleted
                                    select c).Count();
            return examAttemptCount;
        }

        public int GetExamQuestionMaxSort(int ExamId)
        {
            var _context = GetContext().Set<ExaminationQuestion>()
                .Where(x => x.ExaminationId == ExamId && !x.IsDeleted)
                .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (_context == null) ? 0 : _context.SortOrder;
        }

        public bool ResortExamQuestions(List<ExaminationQuestion> modelQuestions, List<ExaminationQuestion> savedQuestions, int UserID)
        {

            this.UnitOfWork.BeginTransaction();
            try {
                foreach (var question in modelQuestions) {
                    var savedQuestionItem = savedQuestions.Where(x => x.Id == question.Id).FirstOrDefault();
                    if (savedQuestionItem != null) {
                        if (question.SortOrder != 0) {
                            savedQuestionItem.SortOrder = question.SortOrder;
                            savedQuestionItem.ModifiedDate = AppHelper.GetCurrentDate();
                            savedQuestionItem.LastModifiedById = UserID;
                        }

                    }


                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                return false;
            }

        }

        /// <summary>
        /// find exam my name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<ExaminationDto> SearchIndependentExamByName(string search)
        {
            var context = this.UnitOfWork.Repository<Examination>();

            var results = context.TableNoTracking.Where(x => x.Name.ToLower().Contains(search.ToLower()) && x.ExamType == ExaminationType.Independent).Select(x => new ExaminationDto
            {
                Id = x.Id,
                Type = x.ExamType,
                Name = x.Name,

            }).ToList();


            return results;
        }

        public void CloneExam(int ExamId, string Name, int UserId)
        {
            var exam = Find(x => x.Id == ExamId && x.IsDeleted == false, false);
            if (exam == null)
                throw new ArgumentNullException("Examination not found");

            ExecuteProcedure<CourseDetailDto>("SP_CLONEEXAM", ExamId, Name, UserId);

        }
    }
}
