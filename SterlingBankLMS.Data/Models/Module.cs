using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Module: TrackableEntity
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CourseId { get; set; }
        public  Course Course { get; set; }
        public  ICollection<Lesson> Lessons  { get; set; }
    }
}