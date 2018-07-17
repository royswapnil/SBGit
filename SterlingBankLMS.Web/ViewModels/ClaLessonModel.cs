namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaLessonModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string LessonContentType { get; set; }
        public int SortOrder { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsCompleted { get; set; }
    }
}