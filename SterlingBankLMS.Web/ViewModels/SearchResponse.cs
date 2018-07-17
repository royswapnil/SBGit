using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class SearchResponse<T> 
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }
    }

   

}