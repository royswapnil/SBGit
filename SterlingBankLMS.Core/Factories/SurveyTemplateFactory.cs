using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class SurveyTemplateFactory : GenericService<SurveyTemplate>
    {
        public SurveyTemplateFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {


        }

        public bool ValidateNameExists(string Name, int Id)
        {
            var existing = Find(x => x.Name == Name && x.Id != Id, false);
            return (existing == null);

        }

        public SurveyQuestion AddorUpdateSurveyQuestion(SurveyQuestion modelQuestion, int UserID, int OrgId)
        {
            var surveyQuestion = new SurveyQuestion();
            if (modelQuestion.Id > 0)
            {
                surveyQuestion = this.GetSurveyQuestionIncludingOptions(modelQuestion.Id);
            }

            var datenow = AppHelper.GetCurrentDate();


            if (surveyQuestion == null)
                return null;

            SurveyTemplate template = new SurveyTemplate();

            if (modelQuestion.Id == 0)
            {
                template = Find(modelQuestion.TemplateId);

            }

            this.UnitOfWork.BeginTransaction();
            try
            {
                if (modelQuestion.Id == 0)
                {
                    template.QuestionCount = (template.QuestionCount + 1);
                    surveyQuestion.OrganizationId = OrgId;
                    surveyQuestion.CreatedById = UserID;
                    surveyQuestion.CreatedDate = datenow;
                    surveyQuestion.SortOrder = GetSurveyQuestionMaxSort(modelQuestion.TemplateId) + 1;

                    var optionList = new List<SurveyQuestionOptions>();
                    foreach (var option in modelQuestion.Options)
                    {
                        option.CreatedById = UserID;
                        option.LastModifiedById = UserID;
                        option.ModifiedDate = AppHelper.GetCurrentDate();
                        option.CreatedDate = AppHelper.GetCurrentDate();
                        option.OrganizationId = OrgId;
                        optionList.Add(option);
                    }
                    surveyQuestion.Options = optionList;
                }

                surveyQuestion.LastModifiedById = UserID;
                surveyQuestion.ModifiedDate = datenow;
                surveyQuestion.AnswerType = modelQuestion.AnswerType;
                surveyQuestion.TemplateId = modelQuestion.TemplateId;
                surveyQuestion.IsMultipleChoice = modelQuestion.IsMultipleChoice;
                surveyQuestion.Question = modelQuestion.Question;

                if (modelQuestion.Id > 0)
                {
                    foreach (var option in modelQuestion.Options)
                    {
                        if (option.Id == 0)
                        {
                            option.CreatedById = UserID;
                            option.LastModifiedById = UserID;
                            option.ModifiedDate = AppHelper.GetCurrentDate();
                            option.CreatedDate = AppHelper.GetCurrentDate();
                            option.OrganizationId = OrgId;
                            surveyQuestion.Options.Add(option);
                        }
                        else
                        {
                            var examOption = surveyQuestion.Options.Where(x => x.Id == option.Id).FirstOrDefault();
                            examOption.Title = option.Title;
                            examOption.Value = option.Value;
                            examOption.IsAnswer = option.IsAnswer;
                            examOption.LastModifiedById = UserID;
                            examOption.ModifiedDate = datenow;
                        }

                    }
                }
                if (surveyQuestion.Id == 0)
                {
                    GetContext().Set<SurveyQuestion>().Add(surveyQuestion);
                }

                this.UnitOfWork.Commit();

                return surveyQuestion;
            }


            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return null;
            }
        }


        public SurveyQuestion GetSurveyQuestionIncludingOptions(int Id)
        {
            var surveyQuestionContext = UnitOfWork.Repository<SurveyQuestion>();
            var questionOptionContext = GetContext().Set<SurveyQuestion>();

            var Question = surveyQuestionContext.GetAllIncluding(x => x.Id == Id, true, x => x.Options).FirstOrDefault();

            return Question;
        }


        public int GetSurveyQuestionMaxSort(int templateId)
        {
            var _context = GetContext().Set<SurveyQuestion>()
                .Where(x => x.TemplateId == templateId && !x.IsDeleted)
                .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (_context == null) ? 0 : _context.SortOrder;
        }


        public IEnumerable<TemplateDto> GetTemplatePagedList(string search, int pageIndex, int pageSize, int organizationId, out int TotalRecords)
        {
            var _context = UnitOfWork.Repository<SurveyTemplate>();
            var _surveyContext = UnitOfWork.Repository<Survey>();

            var query = (from a in _context.TableNoTracking
                         join b in _surveyContext.TableNoTracking on a.Id equals b.TemplateId into j1
                         from j2 in j1.DefaultIfEmpty()
                         where a.OrganizationId == organizationId && !a.IsDeleted
                      && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && a.Name.Contains(search)))
                         group j2 by new { a.Name, a.Id, a.QuestionCount } into grp
                         orderby grp.Key.Id
                         select new TemplateDto { Id = grp.Key.Id, Name = grp.Key.Name, QuestionCount = grp.Key.QuestionCount, SurveyCount = grp.Count() })
                        .Skip(pageIndex * pageSize).Take(pageSize).Future();

            var queryCount = (from a in _context.TableNoTracking
                              where a.OrganizationId == organizationId && !a.IsDeleted
                              select a).DeferredCount().FutureValue();


            var templates = query.ToList();
            TotalRecords = queryCount.Value;

            return templates;
        }


        public int ValidateTemplateForDelete(int id)
        {
            var templateContext = UnitOfWork.Repository<SurveyTemplate>();
            var surveyContext = UnitOfWork.Repository<Survey>();

            var surveyCount = (from c in templateContext.TableNoTracking
                               join uc in surveyContext.TableNoTracking
                               on c.Id equals uc.TemplateId
                               where c.Id == id && !uc.IsDeleted
                               select c).Count();
            return surveyCount;
        }


        public void DeleteTemplate(SurveyTemplate entity, int userID)
        {
            this.UnitOfWork.BeginTransaction();
            try
            {

                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                foreach (var question in entity.Questions)
                {
                    question.IsDeleted = true;
                    question.LastModifiedById = userID;
                    question.ModifiedDate = AppHelper.GetCurrentDate();

                    foreach (var options in question.Options)
                    {
                        options.IsDeleted = true;
                        options.LastModifiedById = userID;
                        options.ModifiedDate = AppHelper.GetCurrentDate();
                    }
                }


                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void DeleteTemplateQuestion(SurveyQuestion entity, int userID)
        {
            var surveyQuestion = new SurveyQuestion();
            var template = Find(entity.TemplateId);

            this.UnitOfWork.BeginTransaction();
            try
            {
                var _context = GetContext().Set<SurveyQuestion>();
                template.QuestionCount = (template.QuestionCount - 1);
                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                foreach (var option in entity.Options)
                {
                    option.IsDeleted = true;
                    option.LastModifiedById = userID;
                    option.ModifiedDate = AppHelper.GetCurrentDate();
                }
                
                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public void DeleteQuizOption(SurveyQuestionOptions entity, int userID)
        {

            try
            {
                var _context = GetContext().Set<SurveyQuestionOptions>();
                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();

                GetContext().SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool ResortExamQuestions(List<SurveyQuestion> modelQuestions, List<SurveyQuestion> savedQuestions, int UserID)
        {

            this.UnitOfWork.BeginTransaction();
            try
            {
                foreach (var question in modelQuestions)
                {
                    var savedQuestionItem = savedQuestions.Where(x => x.Id == question.Id).FirstOrDefault();
                    if (savedQuestionItem != null)
                    {
                        if (question.SortOrder != 0)
                        {
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
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return false;
            }

        }

    }
}
