using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    public class LearningAreaModel : BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}