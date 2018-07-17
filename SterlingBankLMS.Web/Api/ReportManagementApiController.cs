using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SterlingBankLMS.Web.ViewModels;
using DataTables.AspNet.Core;
using System.Xml;
using System.Xml.Linq;
using SterlingBankLMS.Web.Models.IdentityModels;
using Newtonsoft.Json;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/ReportManagement")]
    public class ReportManagementApiController : BaseApiController
    {
        private readonly UserFactory _userFactory;
        private readonly ReportFactory _reportFactory;
        private readonly GroupFactory _groupFactory;
        private readonly GradeFactory _gradeFactory;
        private readonly DepartmentFactory _departmentFactory;
        private readonly UserCourseFactory _userCourseFactory;
       // private readonly ExaminationAttemptFactory _examinationAttemptFactory; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRole;
        private readonly AuditSearchCriteriaDto _auditSearchCriteria;

        public ReportManagementApiController(UserFactory userFactory, IWorkContext workContext,
            IPermissionService permissionService, IUserAccountService userAccountService, IUserRoleService userRole,
            UserCertificateFactory certificateFactory, IUnitOfWork unitOfWork, ReportFactory reportFactory, GroupFactory groupFactory,
            GradeFactory gradeFactory, DepartmentFactory departmentFactory, AuditSearchCriteriaDto auditSearchCriteria, UserCourseFactory userCourseFactory/*, ExaminationAttemptFactory examinationAttemptFactory*/)
        {
            _userFactory = userFactory;
            _workContext = workContext;
            _permissionService = permissionService;
            _userAccountService = userAccountService;
            _userRole = userRole;
            _unitOfWork = unitOfWork;
            _reportFactory = reportFactory;
            _groupFactory = groupFactory;
            _gradeFactory = gradeFactory;
            _departmentFactory = departmentFactory;
            _auditSearchCriteria = auditSearchCriteria;
            _userCourseFactory = userCourseFactory;
           // _examinationAttemptFactory = examinationAttemptFactory;
        }

        [HttpGet]
        [Route("GetAuditTable")]
        public IHttpActionResult GetAuditTable(IDataTablesRequest request)
        {
            var search = JsonConvert.DeserializeObject<AuditSearchCriteriaDto>(Convert.ToString(request.AdditionalParameters["data"]));
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;

            var recordsDto = _reportFactory.GetPagedAuditList(search, request.Length, index, out totalRecords).ToList();
            var records = recordsDto.MapTo<IEnumerable<AuditTrackerDto>, IEnumerable<AuditTrackerModel>>();
            records = records.Where(c => c.ColumnName.ToLower().Contains(request.Search.Value.ToLower()));
            var returnObject = new SearchResponse<AuditTrackerModel>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = records.ToList()
            };
            return Ok(returnObject);

        }


        [HttpGet]
        [Route("GetReportTable")]
        public IHttpActionResult GetReportTable(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var records = _reportFactory.GetReportFields(request.Search.Value, index, request.Length, out totalRecords);
            var returnObject = new SearchResponse<AllReportsDataTable>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = records.ToList()
            };
            return Ok(returnObject);

        }

        [HttpGet]
        [Route("GetGenReportTable")]
        public IHttpActionResult GetGenReportTable(IDataTablesRequest request)
        {
            var search = JsonConvert.DeserializeObject<GenerateReportDto>(Convert.ToString(request.AdditionalParameters["data"]));
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;

            var recordsDto = _reportFactory.GetPagedGenReportList(search, request.Length, index, out totalRecords).ToList();
            if (recordsDto != null && recordsDto.Count() > 0 && recordsDto.FirstOrDefault().StaffName != null)
                recordsDto = recordsDto.Where(c => c.StaffName.ToLower().Contains(request.Search.Value.ToLower())).ToList();
            else if (recordsDto != null && recordsDto.Count() > 0 && recordsDto.FirstOrDefault().Name != null)
                recordsDto = recordsDto.Where(c => c.Name.ToLower().Contains(request.Search.Value.ToLower())).ToList();
            var returnObject = new SearchResponse<GenerateReportDataTableDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = recordsDto.ToList()
            };
            return Ok(returnObject);

        }

        [HttpGet]
        [Route("getallusers")]
        public List<ApplicationUser> GetUsers()
        {
            var data = new ApiResult<List<ApplicationUser>>();
            var result = _userAccountService.GetAllUsers().OrderBy(x => x.LastName).ToList();
            data.Result = result;
            data.HasError = false;
            return data.Result;
        }

        [HttpGet]
        [Route("GetEntityList")]
        public List<EntityDropDown> GetEntityList()
        {
            XmlDocument xmlToSave = new XmlDocument();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\EntityXml\\spexeccdc.xml";
            xmlToSave.Load(baseDirectory);
            XElement element = XElement.Parse(xmlToSave.InnerXml);
            var entitylist = from a in element.Descendants("entitylist")
                             select new EntityDropDown
                             {
                                 id = a.Element("entityId").Value,
                                 text = a.Element("entityname").Value
                             };

            return entitylist.ToList();
        }

        [HttpGet]
        [Route("getGroup")]
        public List<GroupDto> getGroup()
        {
            var result = new ApiResult<List<GroupDto>>();

            var groups = _groupFactory.All(x => !x.IsDeleted, false).Select(x => new GroupDto
            {
                GroupId = x.Id,
                GroupName = x.Name

            }).ToList();
            result.HasError = false;
            return groups.ToList();
        }

        [HttpGet]
        [Route("getGrade")]
        public List<GradeModel> GetGrade()
        {
            var data = new ApiResult<List<GradeModel>>();
            var result = _gradeFactory.All(x => !x.IsDeleted, false).Select(x => new GradeModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            data.Result = result;
            data.HasError = false;
            return data.Result;
        }

        [HttpGet]
        [Route("getDepartment/{GroupId}")]
        public List<DepartmentDropdownListDto> GetDepartment(int GroupId)
        {
            var data = new ApiResult<List<DepartmentDropdownListDto>>();
            var result = _departmentFactory.All((x => !x.IsDeleted && x.Id == GroupId), false).Select(x => new DepartmentDropdownListDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            data.Result = result;
            data.HasError = false;
            return data.Result;
        }

        [HttpGet]
        [Route("getemployeecourseprogress")]
        public IHttpActionResult GetEmpCourseProgress(IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var userCourse = _userCourseFactory.ExecuteProcedure<UserCourseProgressDto>("SP_UserCourseProgress", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

            var obj = new SearchResponse<UserCourseProgressDto>
            {
                draw = request.Draw,
                recordsTotal = userCourse.Count() > 0 ? userCourse[0].TotalRecords : 0,
                recordsFiltered = userCourse.Count() > 0 ? userCourse[0].TotalRecords : 0,
                data = userCourse
            };
            return Ok(obj);
        }

        //[HttpGet]
        //[Route("getemployeeexamprogress")]
        //public IHttpActionResult GetEmpExamProgress( IDataTablesRequest request )
        //{
        //    var index = request.Start == 0 ? 0 : request.Start / request.Length;
        //    var exam = _examinationAttemptFactory.ExecuteProcedure<UserExamProgressDto>("SP_UserExaminationProgress", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

        //    var obj = new SearchResponse<UserExamProgressDto>
        //    {
        //        draw = request.Draw,
        //        recordsTotal = exam.Count() > 0 ? exam[0].TotalRecords : 0,
        //        recordsFiltered = exam.Count() > 0 ? exam[0].TotalRecords : 0,
        //        data = exam
        //    };
        //    return Ok(obj);
        //}
    }
}
