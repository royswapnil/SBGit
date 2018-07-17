using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ModuleViewModel
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        public int HierachicalOrder { get; set; }
        public IEnumerable<TopicViewModel> Topics { get; set; }
    }
}
