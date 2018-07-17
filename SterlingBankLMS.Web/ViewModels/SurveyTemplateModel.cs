using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class SurveyTemplateModel : BaseModel
    {
        public string Name { get; set; }

        public List<QuizQuestionModel> Questions { get; set; }
    }
}