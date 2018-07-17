using SterlingBankLMS.Web.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class BulkUploadModel
    {
        public string FilePath { get; set; }
        public UploadType uploadType { get; set; }
    }
}