using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
   public class FAQ:TrackableEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }
    }
}
