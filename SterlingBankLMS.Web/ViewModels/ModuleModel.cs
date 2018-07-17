using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ModuleModel:BaseModel
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int SortOrder { get; set; }
        public bool IsCompleted { get; set; }
        public List<ModuleLessonModel> Lessons { get; set; }
    }
}