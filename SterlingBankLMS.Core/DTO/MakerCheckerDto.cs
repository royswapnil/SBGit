using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Core.DTO
{
    public class MakerCheckerDto
    {
        public int Id { get; set; }

        public bool IsApproval { get; set; }

        public string  Comments { get; set; }

        public int ActionUserId { get; set; }
    }
}