namespace SterlingBankLMS.Web.ViewModels
{
    public class AssignedCourseModel
    {
        public double? AverageRating { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public string DueDate { get; set; }
        public string ShortDescription { get; set; }
        //public string LearningArea { get; set; }
    }
}