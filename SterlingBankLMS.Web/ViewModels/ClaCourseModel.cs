using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public string Author { get; set; }
        public string DueDate { get; set; }

        public IEnumerable<ClaModuleModel> Modules { get; set; }
    }
}