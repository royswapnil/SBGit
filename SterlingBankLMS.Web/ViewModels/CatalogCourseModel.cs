namespace SterlingBankLMS.Web.ViewModels
{
    public class CatalogCourseModel
    {
        public double? AverageRating { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
    }
}