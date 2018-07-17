using MultipartDataMediaFormatter.Infrastructure;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class AdBannerModel : BaseModel
    {
        public string Title { get; set; }

        public string Size { get; set; }

        public HttpFile ImageUpload { get; set; }

        public string FileUrl { get; set; }
       
    }
}