using System;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Core.DTO
{
    public class TrainingVideoDto
    {
        [Required(ErrorMessage ="Please upload a video file")]
        public string TrainingVideoUrl { get; set; }
        [Required(ErrorMessage ="Please enter the name of video")]
        public string TrainingVideoName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public int TotalRecords { get; set; }
        public string CreatedDateFormat => CreatedDate.ToString("dd-MMM-yyyy");
        //public string empty => "";
    }
}
