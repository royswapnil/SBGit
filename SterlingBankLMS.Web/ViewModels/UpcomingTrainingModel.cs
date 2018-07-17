using SterlingBankLMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class UpcomingTrainingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}