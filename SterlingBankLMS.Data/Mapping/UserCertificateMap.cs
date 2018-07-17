using SterlingBankLMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Mapping
{
    public class UserCertificateMap: EntityTypeConfiguration<UserCertificate>
    {
        public UserCertificateMap()
        {

        }
    }
}
