using AutoMapper;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Globalization;

namespace SterlingBankLMS.Web
{

    public partial class Startup
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        /// <summary>
        /// Configures Application to use Automapper
        /// </summary>
        public static void UseAutomapper()
        {

            _mapperConfiguration = new MapperConfiguration(cfg => {

                //vm to model
                cfg.CreateMap<CourseReviewModel, CourseReview>();
                cfg.CreateMap<CourseModel, Course>();
                cfg.CreateMap<EmployeeModel, Course>();

                //dto to vm
                cfg.CreateMap<UserExamDto, UserExamModel>();
                cfg.CreateMap<UserCourseStatusDto, UserCourseStatusModel>();
                cfg.CreateMap<LessonNameDto, LessonNameModel>();
                cfg.CreateMap<CatalogCourseDto, CatalogCourseModel>()
                                .MapProperty(o => o.ImageUrl, i => CommonHelper.ToAbsolutePath(i.ImageUrl))
                                .MapProperty(o => o.Slug, i => StringExtensions.UrlSlug(i.Name));

                cfg.CreateMap<ClaCourseDto, ClaCourseModel>()
                                .MapProperty(o => o.Slug, i => StringExtensions.UrlSlug(i.Name));

                cfg.CreateMap<CourseModuleNameDto, CourseModuleNameModel>();

                cfg.CreateMap<CourseProgressDto, CourseProgressModel>()
                               .MapProperty(o => o.Slug, n => StringExtensions.UrlSlug(n.Name));

                cfg.CreateMap<EmployeeDetailsDto, EmployeeDetailsModel>();
                cfg.CreateMap<EmployeeDto, EmployeeModel>();
                cfg.CreateMap<EmployeeTrainingRecordDto, EmployeeTrainingRecordModel>();
                cfg.CreateMap<EmployeeCertificateDto, EmployeeCertificateModel>();
                cfg.CreateMap<AssignedCourseDto, AssignedCourseModel>()
                                .MapProperty(o => o.Slug, i => StringExtensions.UrlSlug(i.Name));

                cfg.CreateMap<CourseDetailDto, CourseDetailModel>()
                                .MapProperty(o => o.Slug, i => StringExtensions.UrlSlug(i.Name));

                //cfg.CreateMap<CourseDetailDto, CourseDetailModel>();

                //model to vm
                cfg.CreateMap<ExaminationQuestion, ClaExamQuestionModel>();
                cfg.CreateMap<ExaminationQuestionOption, ClaExamOptionModel>();
                cfg.CreateMap<Survey, ClaSurveyModel>();
                cfg.CreateMap<SurveyQuestion, ClaSurveyQuestionModel>();
                cfg.CreateMap<SurveyQuestionOptions, ClaSurveyOptionModel>();

                cfg.CreateMap<Examination, CourseExaminationModel>();
                cfg.CreateMap<Lesson, ClaLessonModel>();
                cfg.CreateMap<QuizQuestion, ClaQuizQuestionModel>();
                cfg.CreateMap<QuizQuestionOption, ClaQuizOptionModel>();
                cfg.CreateMap<LearningArea, DropdownListModel>();

                cfg.CreateMap<ApplicationUser, EmployeeProfileModel>()
                                .MapProperty(o => o.Grade, n => n.Grade.Name)
                                .MapProperty(o => o.Region, n => n.Region.Name)
                                .MapProperty(o => o.Branch, n => n.Branch.Name)
                                .MapProperty(o => o.DateOfBirth, n => n.DateOfBirth == null ? "" : n.DateOfBirth.Value.ToString("dd MMM yyyy"))
                                .MapProperty(o => o.DateOfEmployment, n => n.DateOfEmployment.HasValue ? n.DateOfEmployment.Value.ToString("dd MMM yyyy") : "")
                                .MapProperty(o => o.Department, n => n.Department.Name);

                cfg.CreateMap<Module, ClaModuleModel>();
                cfg.CreateMap<Course, CourseModel>();

                cfg.CreateMap<LearningArea, LearningAreaModel>();
                cfg.CreateMap<LearningAreaModel, LearningArea>();

                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<ModuleModel, Module>();
                cfg.CreateMap<Lesson, ModuleLessonModel>();
                cfg.CreateMap<ModuleLessonModel, Lesson>();
                cfg.CreateMap<QuizQuestion, QuizQuestionModel>();
                cfg.CreateMap<QuizQuestionModel, QuizQuestion>();
                cfg.CreateMap<QuizQuestionOptionModel, QuizQuestionOption>();

                // LEARNING GROUP MAPPER
                cfg.CreateMap<LearningGroupModel.Group, LearningGroup>();

                cfg.CreateMap<LearningGroupModel.Tags, LearningGroupTag>();

                cfg.CreateMap<LearningGroup, LearningGroupModel.Group>();

                cfg.CreateMap<LearningGroupTag, LearningGroupModel.Tags>();


                cfg.CreateMap<LearningGroupModel.Courses, LearningGroupCourse>();
                cfg.CreateMap<LearningGroupModel.Surveys, LearningGroupSurvey>();
                cfg.CreateMap<LearningGroupModel.Exams, LearningGroupExam>();
                cfg.CreateMap<LearningGroupModel.Trainings, LearningGroupTraining>();


                cfg.CreateMap<ReportDto, Report>();
                cfg.CreateMap<ExaminationModel, Examination>();
                cfg.CreateMap<Examination, ExaminationModel>().ForMember(dest => dest.StartDateFormat, opt => opt.MapFrom(src => src.StartDate != null ? Convert.ToDateTime(src.StartDate).ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.EndDateFormat, opt => opt.MapFrom(src => src.EndDate != null ? Convert.ToDateTime(src.EndDate).ToString("dd/MM/yyyy") : null));
                cfg.CreateMap<QuizQuestionModel, ExaminationQuestion>();
                cfg.CreateMap<ExaminationQuestion, QuizQuestionModel>();
                cfg.CreateMap<QuizQuestionOptionModel, ExaminationQuestionOption>();
                cfg.CreateMap<ExaminationQuestionOption, QuizQuestionOptionModel>();

                cfg.CreateMap<LearningGroupModel.Courses, LearningGroupCourse>();

                //cfg.CreateMap<UserTrainingModel, TrainingGroupModel.Trainings>();
                //cfg.CreateMap<UserTrainingModel, TrainingGroupModel.Tags>();
                //cfg.CreateMap<UserTrainingModel, UserTraining>();

                cfg.CreateMap<TicketModel, TicketDto>();
                cfg.CreateMap<ApplicationUser, UserDto>().MapProperty(o => o.Email, n => n.Email)
                                .MapProperty(o => o.UserId, n => n.Id);

                cfg.CreateMap<SurveyTemplateModel, SurveyTemplate>();
                cfg.CreateMap<SurveyTemplate, SurveyTemplateModel>();
                cfg.CreateMap<QuizQuestionModel, SurveyQuestion>();
                cfg.CreateMap<SurveyQuestion, QuizQuestionModel>();
                cfg.CreateMap<QuizQuestionOptionModel, SurveyQuestionOptions>();
                cfg.CreateMap<SurveyQuestionOptions, QuizQuestionOptionModel>();
                cfg.CreateMap<FAQ, FAQModel>();
                cfg.CreateMap<FAQModel, FAQ>();
                cfg.CreateMap<TrainingRequestDto, TrainingRequest>();
                cfg.CreateMap<TrainingRequest, TrainingRequestDto>();
                cfg.CreateMap<TrainingPeriod, TrainingPeriodDto>();
                cfg.CreateMap<TrainingPeriodDto, TrainingPeriod>();


                cfg.CreateMap<TrainingDto, Training>().MapProperty(o => o.BudgetExpended, n => n.Budget)
               .MapProperty(o => o.AmountPerStaff, n => n.BudgetPerStaff)
               .ForMember(dest => dest.StartPeriod, opt => opt.MapFrom(src => src.StartDateFormat != null ? Convert.ToDateTime(src.StartDateFormat).ToString("dd/MM/yyyy") : null))
               .ForMember(dest => dest.EndPeriod, opt => opt.MapFrom(src => src.EndDateFormat != null ? Convert.ToDateTime(src.EndDateFormat).ToString("dd/MM/yyyy") : null))
               .ForMember(o => o.DurationInMinutes, n => n.MapFrom(z => z.Duration));

                cfg.CreateMap<TrainingModel, Training>().MapProperty(o => o.BudgetExpended, n => n.Budget)
             .MapProperty(o => o.AmountPerStaff, n => n.BudgetPerStaff)
             .ForMember(dest => dest.StartPeriod, opt => opt.MapFrom(src => src.StartDateFormat != null ? Convert.ToDateTime(src.StartDateFormat).ToString("dd/MM/yyyy") : null))
             .ForMember(dest => dest.EndPeriod, opt => opt.MapFrom(src => src.EndDateFormat != null ? Convert.ToDateTime(src.EndDateFormat).ToString("dd/MM/yyyy") : null))
             .ForMember(o => o.DurationInMinutes, n => n.MapFrom(z => z.Duration));

                cfg.CreateMap<TrainingModel, TrainingDto>()
                .ForMember(dest => dest.StartPeriod, opt => opt.MapFrom(src => src.StartDateFormat != null ? Convert.ToDateTime(src.StartDateFormat).ToString("dd/MM/yyyy") : null))
             .ForMember(dest => dest.EndPeriod, opt => opt.MapFrom(src => src.EndDateFormat != null ? Convert.ToDateTime(src.EndDateFormat).ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<Training, TrainingDto>().MapProperty(o => o.Budget, n => n.BudgetExpended)
                .MapProperty(o => o.BudgetPerStaff, n => n.AmountPerStaff)
               .ForMember(o => o.Duration, n => n.MapFrom(z => z.DurationInMinutes));

                cfg.CreateMap<Survey, SurveyModel>().ForMember(dest => dest.SurveyAssoID, opt => opt.MapFrom(
                    y => y.SurveyType == SurveyType.CourseSurvey ? y.CourseId
                    : y.SurveyType == SurveyType.ExamSurvey ? y.ExaminationId
                    : y.SurveyType == SurveyType.TrainingSurvey ? y.TrainingId : null));

                cfg.CreateMap<SurveyModel, Survey>()
                .ForMember(x => x.CourseId, y => y.MapFrom(z => z.SurveyType == SurveyType.CourseSurvey ? z.SurveyAssoID : null))
                 .ForMember(x => x.ExaminationId, y => y.MapFrom(z => z.SurveyType == SurveyType.ExamSurvey ? z.SurveyAssoID : null))
                  .ForMember(x => x.TrainingId, y => y.MapFrom(z => z.SurveyType == SurveyType.TrainingSurvey ? z.SurveyAssoID : null));

                cfg.CreateMap<AdBannerModel, AdBanner>();
                cfg.CreateMap<AdBanner, AdBannerModel>();
                cfg.CreateMap<Notification, NotificationDto>();
                cfg.CreateMap<NotificationModel, NotificationDto>();
                cfg.CreateMap<NotificationDto, Notification>();
                cfg.CreateMap<Notification, NotificationModel>();

                cfg.CreateMap<AdvertDto, AdvertModel>()
                .MapProperty(x => x.StartDate, y => y.StartDate.ToString("dd MMM yyyy"))
                .MapProperty(x => x.EndDate, y => y.EndDate.ToString("dd MMM yyyy"));


                cfg.CreateMap<Advert, AdvertModel>()
              .MapProperty(x => x.StartDate, y => y.StartDate.ToString("dd MMM yyyy"))
              .MapProperty(x => x.EndDate, y => y.EndDate.ToString("dd MMM yyyy"))
                 .MapProperty(x => x.FileUrl, y => y.AdBanner.FileUrl);
                cfg.CreateMap<AdvertModel, Advert>()
            .MapProperty(x => x.StartDate, y => DateTime.ParseExact(y.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
            .MapProperty(x => x.EndDate, y => DateTime.ParseExact(y.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                /*Added by NuvTeq for mapping of Reports*/
                cfg.CreateMap<ReportFieldSortDto, ReportFieldSort>().IgnoreProperties(x => x.Report,
                    y => y.OrganizationId, z => z.Organization, a => a.CreatedBy, a => a.CreatedById,
                    b => b.LastModifiedBy, c => c.LastModifiedById, d => d.ModifiedDate, e => e.CreatedDate, f => f.IsDeleted);

                cfg.CreateMap<ReportScheduleDto, ReportSchedule>().IgnoreProperties(x => x.Report,
                   y => y.OrganizationId, z => z.Organization, a => a.CreatedBy, a => a.CreatedById,
                   b => b.LastModifiedBy, c => c.LastModifiedById, d => d.ModifiedDate, e => e.CreatedDate, f => f.IsDeleted);

                cfg.CreateMap<ReportUsersDto, ReportUsers>().IgnoreProperties(x => x.Report,
                   y => y.OrganizationId, z => z.Organization, a => a.CreatedBy, a => a.CreatedById,
                   b => b.LastModifiedBy, c => c.LastModifiedById, d => d.ModifiedDate, e => e.CreatedDate, f => f.IsDeleted);
                /*Ended*/

            });

            _mapper = _mapperConfiguration.CreateMapper();

        }



        public static IMapper Mapper => _mapper;
    }
}