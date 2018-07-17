using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
   

    public class NotificationReplaceTags
    {
        public bool IsGeneral { get; set; }
        public List<NotificationType> NotificationTypes { get; set; }
        public string Name { get; set; }
        public string ReplaceTag { get; set; }
        public string Information { get; set; }
    }
}