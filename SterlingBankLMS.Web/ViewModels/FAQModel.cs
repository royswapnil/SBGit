using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class FAQModel:BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }
    }
}