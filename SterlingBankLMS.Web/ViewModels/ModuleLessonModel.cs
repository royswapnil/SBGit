using MultipartDataMediaFormatter.Infrastructure;
using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ModuleLessonModel: BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string ContentUrl { get; set; }

        public string LiveContentUrl => ContentUrl;

        public bool IsExternalContent { get; set; }

        public string HtmlContent { get; set; }
        public string MimeType { get; set; }

        public decimal? PassMark { get; set; }

        public int? QuestionNo { get; set; }


        public List<QuizQuestionModel> Questions { get; set; }
        public LessonContentType LessonContentType { get; set; }

        public bool IsVideo => this.LessonContentType == LessonContentType.Video ? true : false;

        public bool IsText => this.LessonContentType == LessonContentType.Text ? true : false;

        public bool IsQuiz => this.LessonContentType == LessonContentType.Quiz ? true : false;

        public bool IsDocument => this.LessonContentType == LessonContentType.Document ? true : false;

        public int? QuizRetakeCount { get; set; }

        public int ModuleId { get; set; }

        public int SortOrder { get; set; }

        public string GetFilePath()
        {
            if (IsExternalContent)
                return ContentUrl;

            if (this.ContentUrl == null)
                return null;

            if (IsVideo)
                return
                   (Utilities.AppConstants.VideoUploadPath + this.OrganizationId + "/" + this.ContentUrl).Replace("~", "");

            if (IsDocument)
                return
                   (Utilities.AppConstants.DocUploadPath + this.OrganizationId + "/"  + this.ContentUrl).Replace("~", "");

            return null;
        }

        public bool IsGradeableContent { get; set; }

        public HttpFile FileUpload { get; set; }


    }
}