using SterlingBankLMS.Web.Models.IdentityModels;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.Utilities
{
    public class PermissionProvider
    {
        public static readonly Permission EmployeeRecords = new Permission { Name = "Manage Employee Records", SystemName = "manageemployeerecords" };
        public static readonly Permission ManageCourse = new Permission { Name = "Manage Course", SystemName = "managecourse" };
        public static readonly Permission ManageLearningGroup = new Permission { Name = "Manage Learning Group", SystemName = "managelearninggroup" };
        public static readonly Permission ManageExamination = new Permission { Name = "Manage Examination", SystemName = "manageexamination" };
        public static readonly Permission ManageSurvey = new Permission { Name = "Manage Survey", SystemName = "managesurvey" };
        public static readonly Permission ManageNotification = new Permission { Name = "Manage Notification", SystemName = "managenotification" };
        public static readonly Permission ManageAdverts = new Permission { Name = "Manage Advert", SystemName = "manageadvert" };
        public static readonly Permission ManageFAQ = new Permission { Name = "Manage FAQ", SystemName = "managefaq" };
        public static readonly Permission AccessLMS = new Permission { Name = "Access LMS", SystemName = "accesslms" };
        public static readonly Permission ManageTraining = new Permission { Name = "Manage Training", SystemName = "managetraining" };
        public static readonly Permission ManageSupport = new Permission { Name = "Manage Support", SystemName = "managesupport" };
        public static readonly Permission ManageUsers = new Permission { Name = "Manage Users", SystemName = "manageusers" };
        public static readonly Permission Reporting = new Permission { Name = "Reporting", SystemName = "reports" };
        public static readonly Permission Auditing = new Permission { Name = "Audit", SystemName = "Audit" };
        public static readonly Permission GeneralMgt = new Permission { Name = "General", SystemName = "generals" };
        public static readonly Permission OrganizationManagement = new Permission { Name = "Organization Management", SystemName = "organizations" };
        public static readonly Permission AccessAdmin = new Permission { Name = "Access Admin Panel", SystemName = "AccessAdmin" };

        public static Dictionary<string, IEnumerable<Permission>> GetSystemDefaultRoles()
        {
            return new Dictionary<string, IEnumerable<Permission>>
            {
                    { AppConstants.Role.Admin,new Permission []{

                        EmployeeRecords,
                        ManageCourse,
                        ManageLearningGroup,
                        ManageExamination,
                        ManageSurvey,
                        ManageNotification,
                        ManageAdverts,
                        AccessLMS,
                        ManageTraining,
                        ManageSupport,
                        ManageUsers,
                        Reporting,
                        GeneralMgt,
                        OrganizationManagement,
                        ManageFAQ,
                        AccessAdmin
                    }
                }
                ,
                     { AppConstants.Role.HR,new Permission []{
                        EmployeeRecords,
                        AccessLMS,
                        GeneralMgt,
                        Reporting,
                        AccessAdmin
                    }
                },
                     { AppConstants.Role.IT,new Permission []{
                        AccessLMS,
                        ManageSupport,
                        ManageUsers,
                        Reporting,
                        AccessAdmin
                    }
                },
                     { AppConstants.Role.Mgt,new Permission []{
                        AccessLMS,
                        Reporting,
                        AccessAdmin
                    }
                },
                     { AppConstants.Role.Employee,new Permission []{
                        AccessLMS
                    }
                }
                //,
                //     {
                //    AppConstants.Role.Auditor,new Permission[]
                //    {
                //        AccessLMS,
                //        AuditTrail
                //    }
                //}
            };
        }
    }
}