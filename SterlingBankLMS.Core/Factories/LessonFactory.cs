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
    /// <summary>
    /// Lesson Persistance Repository
    /// </summary>
    public class LessonFactory : GenericService<Lesson>
    {
        public readonly QuizQuestionFactory _quizQuestionFactory;
        public LessonFactory(IUnitOfWork unitOfWork,QuizQuestionFactory quizQuestionFactory) : base(unitOfWork)
        {
            _quizQuestionFactory = quizQuestionFactory;
        }

        public LessonNameDto GetLessonInCourse(int lessonId, int moduleId, int courseId, int orgId)
        {
            var query = from l in UnitOfWork.Repository<Lesson>().TableNoTracking
                        where l.Id == lessonId && l.ModuleId == moduleId
                        && l.Module.CourseId == courseId
                        && l.Module.Course.OrganizationId == orgId
                        && l.IsDeleted == false
                        select new LessonNameDto { Type = l.LessonContentType, Id = l.Id };

            return query.FirstOrDefault();
        }
        //Top or this
        //public dynamic ValidateLessonInCourse(int lessonId, int moduleId, int courseId, int orgId)
        //{
        //    return ExecuteProcedure<dynamic>("Sp_GetLessonInmoduleIncourse", courseId, moduleId, lessonId, orgId).FirstOrDefault();
        //}

        public LessonDto GetUserLesson(int id, int orgId)
        {
            return ExecuteProcedure<LessonDto>("Sp_GetLesson", id, orgId)
                    .FirstOrDefault();
        }

        public Lesson AddorUpdateLessonContent(Lesson modellesson, int UserID, int OrgId)
        {

            /// insert new lesson or get lesson to update
            var lesson = modellesson.Id == 0 ? modellesson : IncludeFilter(x => x.Questions.Where(y => !y.IsDeleted),
                x => x.Questions.Where(y => !y.IsDeleted).Select(z => z.Options.Where(y => !y.IsDeleted))).Where(x => x.Id == modellesson.Id).FirstOrDefault();

            if (lesson == null)
                return null;

            this.UnitOfWork.BeginTransaction();
            try
            {
                if (lesson.Id == 0)
                {
                    lesson.OrganizationId = OrgId;
                    lesson.CreatedById = UserID;
                    lesson.CreatedDate = AppHelper.GetCurrentDate();
                }

                lesson.LastModifiedById = UserID;
                lesson.ModifiedDate = AppHelper.GetCurrentDate();
                lesson.Description = modellesson.Description;
                

                if (lesson.LessonContentType == LessonContentType.Quiz)
                {
                    lesson.QuizRetakeCount = modellesson.QuizRetakeCount;
                    lesson.IsGradeableContent = modellesson.IsGradeableContent;
                    lesson.PassMark = modellesson.PassMark;
                    lesson.QuestionNo = modellesson.QuestionNo;
                }
                else
                {
                    lesson.QuizRetakeCount = null;
                    lesson.PassMark = null;
                    lesson.QuestionNo = null;
                }


                if (lesson.LessonContentType == LessonContentType.Text)
                {
                    lesson.HtmlContent = modellesson.HtmlContent;
                }


                if (lesson.LessonContentType == LessonContentType.Quiz)
                {
                    if (lesson.Questions == null)
                        lesson.Questions = new List<QuizQuestion>();

                    /// loop through questions if new add, if old update
                    foreach (var questions in modellesson.Questions)
                    {
                        if (questions.Id == 0)
                        {
                            questions.OrganizationId = OrgId;
                            questions.CreatedById = UserID;
                            questions.LastModifiedById = UserID;
                            questions.ModifiedDate = AppHelper.GetCurrentDate();
                            questions.CreatedDate = AppHelper.GetCurrentDate();

                            var optionList = new List<QuizQuestionOption>();
                            foreach (var option in questions.Options)
                            {
                                option.CreatedById = UserID;
                                option.LastModifiedById = UserID;
                                option.ModifiedDate = AppHelper.GetCurrentDate();
                                option.CreatedDate = AppHelper.GetCurrentDate();
                                option.OrganizationId = OrgId;
                                optionList.Add(option);
                            }
                            questions.Options = optionList;
                            lesson.Questions.Add(questions);
                        }
                        else
                        {
                            var savedQuestion = lesson.Questions.Where(x => x.Id == questions.Id).FirstOrDefault();
                            if (savedQuestion.Options == null)
                                savedQuestion.Options = new List<QuizQuestionOption>();

                            if (savedQuestion != null)
                            {
                                savedQuestion.Question = questions.Question;
                                savedQuestion.SortOrder = questions.SortOrder;
                                savedQuestion.LastModifiedById = UserID;
                                savedQuestion.ModifiedDate = AppHelper.GetCurrentDate();
                                savedQuestion.IsMultipleChoice = questions.IsMultipleChoice;
                                savedQuestion.Weight = questions.Weight;

                                // check for updated options or new to add

                                foreach (var option in questions.Options)
                                {
                                    var oldOption = savedQuestion.Options.Where(x => x.Id == option.Id).FirstOrDefault();
                                    if (oldOption != null)
                                    {
                                        oldOption.LastModifiedById = UserID;
                                        oldOption.ModifiedDate = AppHelper.GetCurrentDate();
                                        oldOption.IsAnswer = option.IsAnswer;
                                        oldOption.Value = option.Value;
                                        

                                    }
                                    else
                                    {
                                        option.CreatedById = UserID;
                                        option.OrganizationId = OrgId;
                                        option.LastModifiedById = UserID;
                                        option.CreatedDate = AppHelper.GetCurrentDate();
                                        option.ModifiedDate = AppHelper.GetCurrentDate();

                                        savedQuestion.Options.Add(option);
                                    }
                                }

                            }
                        }
                    }

                }


                if (lesson.LessonContentType == LessonContentType.Video || lesson.LessonContentType == LessonContentType.Document)
                {
                    if (modellesson.ContentUrl != null)
                        lesson.ContentUrl = modellesson.ContentUrl;
                    lesson.IsExternalContent = modellesson.IsExternalContent;
                }

                if (lesson.Id == 0)
                {
                    this.Add(lesson);
                }
                else
                {
                    this.Update(lesson);
                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();

                return lesson;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return null;
            }
        }

        public QuizQuestion AddorUpdateQuizContent(QuizQuestion modelQuestion, int UserID, int OrgId)
        {

            /// insert new quiz or get quiz to update
            /// 
            var _context = UnitOfWork.Repository<QuizQuestion>();

            var question = modelQuestion.Id == 0 ? modelQuestion : _context.GetAllIncluding(x => x.Id == modelQuestion.Id, true, x => x.Options.Where(y => !y.IsDeleted)).FirstOrDefault();

            if (question == null)
                return null;

            this.UnitOfWork.BeginTransaction();
            try
            {
                if (question.Id == 0)
                {
                    question.OrganizationId = OrgId;
                    question.CreatedById = UserID;
                    question.LastModifiedById = UserID;
                    question.ModifiedDate = AppHelper.GetCurrentDate();
                    question.CreatedDate = AppHelper.GetCurrentDate();
                    question.SortOrder = GetQuizMaxSort(question.LessonId) + 1;

                    var optionList = new List<QuizQuestionOption>();
                    foreach (var option in question.Options)
                    {
                        option.CreatedById = UserID;
                        option.LastModifiedById = UserID;
                        option.ModifiedDate = AppHelper.GetCurrentDate();
                        option.CreatedDate = AppHelper.GetCurrentDate();
                        option.OrganizationId = OrgId;
                        optionList.Add(option);
                    }
                    question.Options = optionList;
                    
                    _context.Create(question);
                }
                else
                {
                    question.Question = modelQuestion.Question;
                    question.Weight = modelQuestion.Weight;
                    question.AnswerType = modelQuestion.AnswerType;
                    question.IsMultipleChoice = modelQuestion.IsMultipleChoice;
                    question.LastModifiedById = UserID;
                    question.ModifiedDate = AppHelper.GetCurrentDate();
                    

                    if (question.Options == null)
                        question.Options = new List<QuizQuestionOption>();


                    // loop through options if doesnt exist delete, if new add and if old update
                    // check for deleted options
                    foreach (var option in question.Options)
                    {
                        if (!modelQuestion.Options.Any(x => x.Id == option.Id))
                        {
                            option.IsDeleted = true;
                            option.LastModifiedById = UserID;
                            option.ModifiedDate = AppHelper.GetCurrentDate();
                        }
                    }

                    // check for updated options or new to add

                    foreach (var option in modelQuestion.Options)
                    {
                        var oldOption = question.Options.Where(x => x.Id == option.Id).FirstOrDefault();
                        if (oldOption != null)
                        {
                            oldOption.LastModifiedById = UserID;
                            oldOption.ModifiedDate = AppHelper.GetCurrentDate();
                            oldOption.IsAnswer = option.IsAnswer;
                            oldOption.Title = option.Title;
                            oldOption.Value = option.Value;
                            
                            
                        }
                        else
                        {
                            option.CreatedById = UserID;
                            option.OrganizationId = OrgId;
                            option.LastModifiedById = UserID;
                            option.CreatedDate = AppHelper.GetCurrentDate();
                            option.ModifiedDate = AppHelper.GetCurrentDate();
                            question.Options.Add(option);
                        }
                    }
                    _context.Update(question);

                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();

                return question;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                return null;
            }
        }


        public void DeleteLesson(Lesson entity, int userID)
        {
            this.UnitOfWork.BeginTransaction();
            try
            {
                foreach (var question in entity.Questions)
                {
                    question.IsDeleted = true;
                    question.LastModifiedById = userID;
                    question.ModifiedDate = AppHelper.GetCurrentDate();
                }

                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();
                this.Update(entity);
                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public QuizQuestionOption GetQuizQuestionOption(int Id)
        {
            return GetContext().Set<QuizQuestionOption>().Where(x => x.Id == Id).FirstOrDefault();
        }

        public void DeleteQuizQuestion(QuizQuestion entity, int userID)
        {
            try
            {
                var _context = GetContext().Set<QuizQuestion>();
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

        public void DeleteQuizOption(QuizQuestionOption entity, int userID)
        {

            try
            {
                var _context = GetContext().Set<QuizQuestionOption>();
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

        public int GetModuleLessonMaxSort(int moduleID)
        {
            var _context = GetContext().Set<Lesson>()
                .Where(x => x.ModuleId == moduleID && !x.IsDeleted)
                .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (_context == null) ? 0 : _context.SortOrder;
        }

        public int GetQuizMaxSort(int lessonId)
        {
            var _context = GetContext().Set<QuizQuestion>()
                .Where(x => x.LessonId == lessonId && !x.IsDeleted)
                .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (_context == null) ? 0 : _context.SortOrder;
        }

        public bool ResortLessonQuizQuestions(Lesson modelLesson, int UserID)
        {

            this.UnitOfWork.BeginTransaction();
            try
            {
                var savedQuestions = _quizQuestionFactory.GetLessonQuizQuestions(modelLesson.Id);
                foreach (var question in modelLesson.Questions)
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