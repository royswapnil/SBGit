using SterlingBankLMS.Web.Utilities.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ApiResult<T>
    {
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public T Result { get; set; }

        public static ApiResult<T> Create()
        {
            return new ApiResult<T>();
        }
    }

    public class CollectionResult<T>
    {
        private IEnumerable<T> _items;
        public int Total { get; set; }
        public IEnumerable<T> Records { get { return _items; } }
        public CollectionResult(IEnumerable<T> items)
        {
            _items = items;
        }
    }

    public class BaseSearchModel
    {
        public string Keywords { get; set; }
        public int? PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class CourseSearchModel : BaseSearchModel
    {
        public CourseFilter? Filter { get; set; }
        public int? FilterBy { get; set; }
    }

    public class EmployeSearchModel: BaseSearchModel
    {
        public int? Group { get; set; }
    }
}