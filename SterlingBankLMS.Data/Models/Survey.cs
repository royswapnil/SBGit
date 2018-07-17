using SterlingBankLMS.Data.Models.Enums;
using System.Collections;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Survey: OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public int? ExaminationId { get; set; }
        public Examination Examination { get; set; }
        public int TemplateId { get; set; }

        public SurveyTemplate Template { get; set; }

        public Training Training { get; set; }

        public int? TrainingId { get; set; }
        public SurveyType SurveyType { get; set; }

        public bool IsPublished { get; set; }
    }
}