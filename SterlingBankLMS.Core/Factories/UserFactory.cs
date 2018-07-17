using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class UserFactory : GenericService<User>
    {
        public UserFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<EmployeeDto> GetEmployeeList(int pageIndx, int? pageSize, int organizationId, string query, int? grp)
        {
            return ExecuteProcedure<EmployeeDto>("SP_GetEmployeeList",
                     pageIndx, pageSize, organizationId, query, grp).ToList();
        }


        public EmployeeDetailsDto GetEmployeeDetails(int id, int organizationId)
        {
            return ExecuteProcedure<EmployeeDetailsDto>("SP_GetEmployeeDetails", id, organizationId).FirstOrDefault();
        }

        public IEnumerable<EmployeeTrainingRecordDto> GetEmployeeTrainingRecords(int id, int organizationId)
        {
            return ExecuteProcedure<EmployeeTrainingRecordDto>("Sp_GetEmployeeTrainingRecords", id, organizationId);
        }

        public IEnumerable<UserCertificateDto> GetEmployeeCertificates(int id, int organizationId)
        {
            return ExecuteProcedure<UserCertificateDto>("Sp_GetEmployeeCertificates", id, organizationId);
        }
        public List<UserDto> GetUserByRole( string roleName, int organizationId)
        {
            return ExecuteProcedure<UserDto>("Sp_GetUsersByRoleOrg", roleName, organizationId).ToList();
        }

        public bool ValidateUserIsLineManager(int LineManagerId)
        {
            int returnItem = GetContext().SqlQuery<int>("select Top 1 id from AspNetUsers where LineManagerId = " + LineManagerId).FirstOrDefault();
         
            if(returnItem == 0)
                return false;

            return true;
        }
    }
}