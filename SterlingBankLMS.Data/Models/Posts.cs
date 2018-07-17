using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models
{
    public class Posts : TrackableEntity
    {
        public string Message { get; set; }
        public DateTime? PostedDate { get; set; }
    }
}
