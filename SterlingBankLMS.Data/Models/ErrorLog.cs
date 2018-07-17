namespace SterlingBankLMS.Data.Models.Entities
{
    public class ErrorLog : BaseEntity
    {
        public string Message { get; set; }
        public string Page { get; set; }

        public string ErrorCode { get; set; }
    }
}