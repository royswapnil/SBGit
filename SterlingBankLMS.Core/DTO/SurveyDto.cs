using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TemplateId { get; set; }
        public string Template { get; set; }
        public int UsersTaken { get; set; }
        public SurveyType SurveyType { get; set; }

        public string SurveyTypeName => Helper.AppHelper.GetDescription(this.SurveyType);

    }
}
