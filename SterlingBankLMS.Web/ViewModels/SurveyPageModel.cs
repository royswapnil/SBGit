using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class SurveyPageModel
    {
        public int? Id { get; set; }

        public bool New { get; set; }

        public int SurveyType { get; set; }
    }
}