using SterlingBankLMS.Web.ViewModels;
using System;

namespace SterlingBankLMS.Web.Utilities.Extensions
{
    public static class SearchExtension
    {
        public static void ValidateSearchQuery(this BaseSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException();

            if (searchModel.PageIndex <= 0)
                searchModel.PageIndex = 1;

            if (searchModel.PageSize <= 0 || searchModel.PageSize > AppConstants.Paging.MaxPageSize)
                searchModel.PageSize = AppConstants.Paging.DefaultPageSize;
        }
    }
}