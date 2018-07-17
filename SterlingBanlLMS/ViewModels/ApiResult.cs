namespace SterlingBankLMS.Web.ViewModels
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
           
        }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public T Result { get; set; }
    }
}