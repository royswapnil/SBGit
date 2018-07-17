using DataTables.AspNet.Core;
using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/organization")]

    public class OrganizationController : BaseApiController
    {
        private readonly GroupFactory _groupFactory;
        private readonly GradeFactory _gradeFactory;
        private readonly RegionFactory _regionFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRoleService;
        private readonly IWorkContext _workContext;
        private readonly DepartmentFactory _departmentFactory;
        private readonly OrganizationFactory _organizationFactory;
        private readonly BranchFactory _branchFactory;


        public OrganizationController( GroupFactory groupFactory, GradeFactory gradeFactory, RegionFactory regionFactory, IUserAccountService userAccountService
            , IUserRoleService userRoleService, IWorkContext workContext, DepartmentFactory departmentFactory, OrganizationFactory organizationFactory, IUnitOfWork unitOfWork, BranchFactory branchFactory )
        {
            _groupFactory = groupFactory;
            _gradeFactory = gradeFactory;
            _regionFactory = regionFactory;
            _userAccountService = userAccountService;
            _userRoleService = userRoleService;
            _workContext = workContext;
            _departmentFactory = departmentFactory;
            _organizationFactory = organizationFactory;
            _unitOfWork = unitOfWork;
            _branchFactory = branchFactory;
        }


        [Route("GetGroups")]
        public IHttpActionResult GetGroups()
        {

            var response = new ApiResult<List<Group>>();
            var group = _groupFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId, false);

            response.Result = group;
            return Ok(response);
        }

        [Route("GetBranches")]
        public IHttpActionResult GetBranches()
        {

            var response = new ApiResult<List<Branch>>();
            var branch = _branchFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId, false);

            response.Result = branch;
            return Ok(response);
        }

        [Route("GetGrades")]
        public IHttpActionResult GetGrades()
        {
            var response = new ApiResult<List<Grade>>();
            var grade = _gradeFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId, false);
            response.Result = grade;
            return Ok(response);
        }

        [Route("GetRoles")]
        public IHttpActionResult GetRoles()
        {

            var response = new ApiResult<List<ApplicationRole>>();
            var roles = _userRoleService.GetRoles().ToList();
            response.Result = roles;
            return Ok(response);
        }


        [Route("GetRegion")]
        public IHttpActionResult GetRegion()
        {
            var response = new ApiResult<List<Region>>();
            var region = _regionFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId, false);
            response.Result = region;
            return Ok(response);
        }

        [Route("GetDepartmentNames")]
        public IHttpActionResult GetDepartmentNames()
        {
            var response = new ApiResult<List<Department>>();
            var dept = _departmentFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId, false);
            response.Result = dept;
            return Ok(response);
        }

        [Route("GetDepartmentByGroupId")]
        public IHttpActionResult GetDepartment( int Id )
        {
            var response = new ApiResult<List<Department>>();
            var department = _departmentFactory.All(x => !x.IsDeleted && x.OrganizationId == _workContext.User.OrganizationId && x.GroupId == Id, false);
            response.Result = department;
            return Ok(response);
        }

        [HttpGet]
        [Route("allorganization")]
        public IHttpActionResult Allorganization( IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var totalRecords = 0;


            var organizations = _organizationFactory.GetAllOrganization(request.Length, index, request.Search.Value, out totalRecords).ToList();
            var obj = new SearchResponse<OrganizationDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = organizations
            };
            return Ok(obj);
        }

        [HttpGet]
        [Route("getorganization")]
        public IHttpActionResult Getorganization( IDataTablesRequest request )
        {
            var model = JsonConvert.DeserializeObject<GetOrganization>(Convert.ToString(request.AdditionalParameters["data"]));
            var organizationId = model.id;
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var totalRecords = 0;

            var org = _organizationFactory.GetOrganizationCourses(organizationId, request.Length, index, request.Search.Value, out totalRecords);

            var obj = new SearchResponse<CourseDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = org
            };
            return Ok(obj);

            
        }

        [HttpGet]
        [Route("getorganizationdetails")]
        public IHttpActionResult GetorganizationD( int id )
        {
            var response = new ApiResult<OrganizationDto>();

            var org = _organizationFactory.Find(id);
            if (org != null)
            {
                var o = new OrganizationDto();
                o.CreatedDate = org.CreatedDate;
                o.Email = org.Email;
                o.LogoUrl = org.LogoUrl;
                o.OrganizationalStatus = org.OrganizationalStatus;
                o.OrganizationId = org.Id;
                o.OrganizationName = org.Name;
                o.SubDomain = org.SubDomain;

                response.HasError = false;
                response.Result = o;
            }
            else
            {
                response.HasError = true;
                response.Message = "Sorry, this organization does not exist";
            }
            return Ok(response);
        }

    }

}