using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaModuleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsCompleted => Lessons.All(x => x.IsCompleted);
        public IEnumerable<ClaLessonModel> Lessons { get; set; }

        public ClaModuleModel()
        {
        }
    }
}