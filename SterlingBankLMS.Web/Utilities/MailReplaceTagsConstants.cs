using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.Utilities
{
    public class MailReplaceTagsConstants
    {
        public static readonly HashSet<NotificationReplaceTags> GeneralTags = new HashSet<NotificationReplaceTags>()
        {
            new NotificationReplaceTags
            {
              Name ="FirstName",
              IsGeneral =true,
              ReplaceTag ="{FirstName}",
              Information ="Recipients First Name"
            },

            new NotificationReplaceTags
            {
                Name ="LastName",
                IsGeneral =true,
                ReplaceTag ="{LastName}",
                Information ="Recipients Last Name"
            },
            new NotificationReplaceTags
            {
                Name ="FullName",
                IsGeneral =true,
                ReplaceTag ="{FullName}",
                Information ="Recipients Full Name"
            },
            new NotificationReplaceTags
            {
                Name ="ActionUser",
                IsGeneral =true,
                ReplaceTag ="{ActionUser}",
                Information ="Full name of user who performed action"
            },
            new NotificationReplaceTags
            {
                Name ="DateCreated",
                IsGeneral =true,
                ReplaceTag ="{DateCreated}",
                Information = "Date item was created"
            },
            new NotificationReplaceTags
            {
                Name ="OrganizationName",
                IsGeneral =true,
                ReplaceTag ="{OrganizationName}",
                Information = "Company Name"
            },
            new NotificationReplaceTags
            {
                Name ="LastModifiedDate",
                IsGeneral =true,
                ReplaceTag ="{LastModifiedDate}",
                Information = "Date item was last edited"
            },
            new NotificationReplaceTags
            {
                Name ="ApplicationName",
                IsGeneral =true,
                ReplaceTag ="{AppName}",
                Information = "Application custom name"
            },
            new NotificationReplaceTags
            {
                Name ="ActionURL",
                IsGeneral =true,
                ReplaceTag ="{ActionURL}",
                Information = "Redirect user to action within mail"
            },
            new NotificationReplaceTags
            {
                Name ="IgnoreMail",
                IsGeneral =true,
                ReplaceTag ="{IgnoreMail}",
                Information = "Allow recipient ignore future mails like this"
            },
            new NotificationReplaceTags
            {
                Name ="Name",
                IsGeneral =true,
                ReplaceTag ="{Name}",
                Information = "Name of item acted on"
            }

        };

        public static readonly HashSet<NotificationReplaceTags> NotificationSpecificTags = new HashSet<NotificationReplaceTags>()
        {
            new NotificationReplaceTags{
                Name ="Comments",
                ReplaceTag ="{Message}",
                Information = "Comments or reply made by user who performed action",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.NewTrainingRequest,
                    NotificationType.TrainingRequestApprovedByAdmin,
                    NotificationType.TrainingRequestApprovedByLineManager,
                    NotificationType.TrainingRequestRejectedByAdmin,
                    NotificationType.TrainingRequestRejectedByLineManager,
                    NotificationType.NewDiscussionReply,
                    NotificationType.NewModerateComment,
                    NotificationType.SupportTicketClosed,
                    NotificationType.NewAdminSupportTicket,
                    NotificationType.NewITSupportTicket
                }
            },

            new NotificationReplaceTags{
                Name ="Title",
                ReplaceTag ="{Title}",
                Information = "Subject or title of raised issue",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.NewAdminSupportTicket,
                    NotificationType.NewITSupportTicket,
                    NotificationType.SupportTicketClosed
                }
            },

            new NotificationReplaceTags
            {
                Name ="StartDate",
                ReplaceTag ="{StartDate}",
                Information = "Start date or period of item",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.ExamAssignment,
                    NotificationType.TrainingAssignment
                }
            },
            new NotificationReplaceTags
            {
                Name ="EndDate",
                ReplaceTag ="{EndDate}",
                Information = "End date, Due date or End period of item",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.ExamAssignment,
                    NotificationType.TrainingAssignment,
                    NotificationType.CourseAssignment,
                    NotificationType.CourseInactivity
                }

            },
            new NotificationReplaceTags
            {
                Name ="Count",
                ReplaceTag ="{Count}",
                Information = "Count of number of actions pending attention",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.PendingModerateComments,
                    NotificationType.PendingTrainingRequests,
                    NotificationType.UnOpenedTicket,
                    NotificationType.PendingUserProfileChange,
                    NotificationType.TrainingRequestPendingAdminApproval
                }
            },

            new NotificationReplaceTags
            {
                Name ="PreviousComment",
                ReplaceTag ="{PrevComment}",
                Information = "Comment or Message being replied to by user",
                NotificationTypes = new List<NotificationType>(){
                    NotificationType.NewDiscussionReply,
                    NotificationType.TicketReply,
                    NotificationType.NewModerateComment
                }
            },

            new NotificationReplaceTags
             {
                 Name ="LessonName",
                 ReplaceTag ="{LessonName}",
                 Information = "Lesson Name being commented on",
                    NotificationTypes = new List<NotificationType>(){
                    NotificationType.NewDiscussionReply,
                    NotificationType.NewModerateComment
                }
            },

            new NotificationReplaceTags
              {
                  Name ="CourseName",
                  ReplaceTag ="{CourseName}",
                  Information = "Course Name being commented on",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.NewDiscussionReply,
                    NotificationType.NewModerateComment
                }
            },

            new NotificationReplaceTags
              {
                  Name ="GroupName",
                  ReplaceTag ="{GroupName}",
                  Information = "Group Name being commented on",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.TrainingBudgetDepletion50percent,
                      NotificationType.TrainingBudgetDepletion90percent,
                      NotificationType.TrainingBudgetDepletion100percent
                }
            },

            new NotificationReplaceTags
              {
                  Name ="NewBudget",
                  ReplaceTag ="{NewBudget}",
                  Information = "New budget",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.BudgetChange
                }
            },

            new NotificationReplaceTags
              {
                  Name ="Duration",
                  ReplaceTag ="{Duration}",
                  Information = "Time to upcoming training",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.UpcomingTrainingPeriod
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentCourse",
                  ReplaceTag ="{CurrentCourse}",
                  Information = "Current user ongoing courses",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentWeekExamination",
                  ReplaceTag ="{CurrentWeekExam}",
                  Information = "Exams starting in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="NextWeekExamination",
                  ReplaceTag ="{NextWeekExam}",
                  Information = "Exams starting next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentWeekSurveys",
                  ReplaceTag ="{CurrentWeekSurvey}",
                  Information = "Surveys starting in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="NextWeekSurveys",
                  ReplaceTag ="{NextWeekSurvey}",
                  Information = "Surveys starting next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentWeekCourseEnd",
                  ReplaceTag ="{CurrentWeekCourseEnd}",
                  Information = "Courses ending in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="NextWeekCourseEnd",
                  ReplaceTag ="{NextWeekCourseEnd}",
                  Information = "Courses ending next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentWeekExamEnd",
                  ReplaceTag ="{CurrentWeekExamEnd}",
                  Information = "Exams ending in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },


            new NotificationReplaceTags
            {
                  Name ="NextWeekExamEnd",
                  ReplaceTag ="{NextWeekExamEnd}",
                  Information = "Exams ending next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },


            new NotificationReplaceTags
            {
                  Name ="CurrentWeekSurveyEnd",
                  ReplaceTag ="{CurrentWeekSurveyEnd}",
                  Information = "Surveys ending in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },


            new NotificationReplaceTags
            {
                  Name ="NextWeekSurveyEnd",
                  ReplaceTag ="{NextWeekSurveyEnd}",
                  Information = "Surveys ending next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },


            new NotificationReplaceTags
            {
                  Name = "CurrentWeekTraining",
                  ReplaceTag ="{CurrentWeekTraining}",
                  Information = "Training ongoing for current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },


            new NotificationReplaceTags
            {
                  Name ="NextWeekTraining",
                  ReplaceTag ="{NextWeekTraining}",
                  Information = "Trainings next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="CurrentWeekTrainingEnd",
                  ReplaceTag ="{CurrentWeekTrainingEnd}",
                  Information = "Training ending in current week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="NextWeekTrainingEnd",
                  ReplaceTag ="{NextWeekTrainingEnd}",
                  Information = "Training ending next week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="MostTakenCourse",
                  ReplaceTag ="{MostTakenCourse}",
                  Information = "Courses taken by most users this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="MostTakenCourseInRegion",
                  ReplaceTag ="{MostTakenCourseInRegion}",
                  Information = "Courses taken by most users in recipients region this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="MostTakenCourseInGroup",
                  ReplaceTag ="{MostTakenCourseInGroup}",
                  Information = "Courses taken by most users in recipients group this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="MostTakenTraining",
                  ReplaceTag ="{MostTakenTraining}",
                  Information = "Trainings taken by most users this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

            new NotificationReplaceTags
            {
                  Name ="MostTakenTrainingInRegion",
                  ReplaceTag ="{MostTakenTrainingInRegion}",
                  Information = "Trainings taken by most users in recipients region this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            },

             new NotificationReplaceTags
            {
                  Name ="MostTakenTrainingInGroup",
                  ReplaceTag ="{MostTakenTrainingInGroup}",
                  Information = "Trainings taken by most users in recipients group this week",
                  NotificationTypes = new List<NotificationType>(){
                    NotificationType.WeeklyUpdate
                }
            }

        };
    };
  }

