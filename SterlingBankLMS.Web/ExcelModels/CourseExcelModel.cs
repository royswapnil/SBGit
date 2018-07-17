using ExcelManager;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ExcelModels
{
    public class CourseExcelModel
    {

        public class LearningArea
        {
            public int ProcessId { get; set; }
            [ExcelReaderCell("Id")]
            public int Id { get; set; }
            [ExcelReaderCell("Name")]
            public string Name { get; set; }
           
        }

        public class Course
        {
            public int ProcessId { get; set; }

            [ExcelReaderCell("Id")]
            public int Id { get; set; }
            [ExcelReaderCell("Name")]
            public string Name { get; set; }

            [ExcelReaderCell("Description")]
            public string Description { get; set; }

            [ExcelReaderCell("ShortDescription")]
            public string ShortDescription { get; set; }

            [ExcelReaderCell("ImageUrl")]
            public string ImageUrl { get; set; }

            [ExcelReaderCell("DueDate")]
            public DateTime? DueDate { get; set; }

            [ExcelReaderCell("LearningAreaId")]
            public int LearningAreaId { get; set; }

            [ExcelReaderCell("LearningArea")]
            public string LearningArea { get; set; }

            [ExcelReaderCell("Overview")]
            public string Overview { get; set; }

            [ExcelReaderCell("Objectives")]
            public string Objectives { get; set; }

            [ExcelReaderCell("EstimatedDuration")]
            public int EstimatedDuration { get; set; }

            [ExcelReaderCell("HoursPerWeek")]
            public int HoursPerWeek { get; set; }

            [ExcelReaderCell("IsPublished")]
            public bool IsPublished { get; set; }

            [ExcelReaderCell("HasCertificate")]
            public bool HasCertificate { get; set; }
        }


        public class Module
        {
            public int ProcessId { get; set; }

            [ExcelReaderCell("Id")]
            public int Id { get; set; }

            [ExcelReaderCell("Course")]
            public int Course { get; set; }

            [ExcelReaderCell("Name")]
            public string Name { get; set; }

        }

        public class Lesson
        {
            public int ProcessId { get; set; }

            [ExcelReaderCell("Id")]
            public int Id { get; set; }

            [ExcelReaderCell("Module")]
            public int Module { get; set; }

            [ExcelReaderCell("Name")]
            public string Name { get; set; }

            [ExcelReaderCell("Description")]
            public string Description { get; set; }

            [ExcelReaderCell("ContentUrl")]
            public string ContentUrl { get; set; }

            [ExcelReaderCell("IsExternalContent")]
            public bool IsExternalContent { get; set; }

            [ExcelReaderCell("HtmlContent")]
            public string HtmlContent { get; set; }

            [ExcelReaderCell("LessonContentType")]
            public string LessonContentType { get; set; }

            public int ContentType => GetContentType();

            [ExcelReaderCell("QuizRetakeCount")]
            public int QuizRetakeCount { get; set; }

            [ExcelReaderCell("Gradeable")]
            public bool Gradeable { get; set; }

            private int GetContentType()
            {
                try
                {
                    LessonContentType? outputType = null;
                    switch (LessonContentType.Trim().ToLower())
                    {
                        case "video": outputType = Data.Models.Enums.LessonContentType.Video; break;
                        case "document": outputType = Data.Models.Enums.LessonContentType.Document; break;
                        case "quiz": outputType = Data.Models.Enums.LessonContentType.Quiz; break;
                        case "text": outputType = Data.Models.Enums.LessonContentType.Text; break;
                    }
                    return (int)outputType;
                }
                catch(Exception ex)
                {
                    throw new ArgumentNullException();
                }
            }
            
        }


        public class QuizQuestions
        {
            public int ProcessId { get; set; }

            [ExcelReaderCell("Id")]
            public int Id { get; set; }

            [ExcelReaderCell("Lesson")]
            public int Lesson { get; set; }

            [ExcelReaderCell("Question")]
            public string Question { get; set; }


            [ExcelReaderCell("QuizAnswerType")]
            public string QuizAnswerType { get; set; }

            public int AnswerType => GetAnswerType();

            [ExcelReaderCell("IsMultipleChoice")]
            public bool IsMultipleChoice { get; set; }

            public bool MultipleChoiceCheck => CheckMultiple();

            [ExcelReaderCell("Weight")]
            public int Weight { get; set; }

            public bool CheckMultiple()
            {
                if (AnswerType == (int)Data.Models.Enums.AnswerType.CheckBox)
                {
                    return IsMultipleChoice;
                }
                else
                {
                    return false;
                }
            }

            private int GetAnswerType()
            {
                try
                {
                    AnswerType? outputType = null;
                    switch (QuizAnswerType.Trim().ToLower())
                    {
                        case "select": outputType = Data.Models.Enums.AnswerType.Select; break;
                        case "radio": outputType = Data.Models.Enums.AnswerType.Radio; break;
                        case "checkbox": outputType = Data.Models.Enums.AnswerType.CheckBox; break;
                    }
                    return (int)outputType;
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException();
                }
              
            }
        }


        public class QuizQuestionOption
        {
            public int ProcessId { get; set; }

            [ExcelReaderCell("Id")]
            public int Id { get; set; }

            [ExcelReaderCell("Question")]
            public int Question { get; set; }

            [ExcelReaderCell("Title")]
            public string Title { get; set; }

            [ExcelReaderCell("Value")]
            public string Value { get; set; }

            [ExcelReaderCell("IsAnswer")]
            public bool IsAnswer { get; set; }

        }
    }
}