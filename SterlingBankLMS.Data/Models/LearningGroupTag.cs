
using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroupTag : BaseEntity
    {
       
        public int? LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }
        public string Name { get; set; }
        public LearningGroupTagType TagType { get; set; }
        public int TagValue { get; set; }
    }
}
