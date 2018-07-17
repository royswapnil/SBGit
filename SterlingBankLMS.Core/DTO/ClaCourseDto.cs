using System;

namespace SterlingBankLMS.Core.DTO
{
    public class ClaCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public string Author { get; set; }
    }

    public class ModuleCourseDto
    {
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }
    }
}