namespace SterlingBankLMS.Data.Models.Entities
{
    public class ForumTopics : OrganizationalBaseEntity
    {
        public string TopicName { get; set; }
        public ForumCategory ForumCat { get; set; }
        public bool IsActive { get; set; }
    }
}