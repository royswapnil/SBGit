using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string TopicName { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime DueDate { get; set; }
    }
}