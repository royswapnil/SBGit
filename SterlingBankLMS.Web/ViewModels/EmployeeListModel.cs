namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeListModel
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public string Branch { get; set; }
        //[JsonIgnore]
        public int TotalCount { get; set; }
    }
}