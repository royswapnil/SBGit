using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class SurveyModel:BaseModel
    {
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SurveyAssoID { get; set; }
        public SurveyType SurveyType { get; set; }
    }
}