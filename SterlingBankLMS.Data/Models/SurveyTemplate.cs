using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class SurveyTemplate : OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public ICollection<SurveyQuestion> Questions { get; set; }

        public int QuestionCount { get;  set; }
    }
}
