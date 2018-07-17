using DataTables.AspNet.Core;
using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/general")]
    public class GeneralHrController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAcct;
        private readonly IUserRoleService _userRole;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserFactory _userFactory;
        private readonly BranchFactory _branchFactory;
        private readonly RegionFactory _regionFactory;
        private readonly GroupFactory _groupFactory;
        private readonly LinemanagerFactory _linemanagerFactory;
        private readonly DepartmentFactory _departmentFactory;

        public GeneralHrController( IWorkContext workContext, IUserAccountService userAcct, UserFactory userFactory, IUserRoleService userRole, BranchFactory branchFactory, RegionFactory regionFactory, GroupFactory groupFactory, LinemanagerFactory linemanagerFactory, DepartmentFactory departmentFactory, IUnitOfWork unitOfWork )
        {
            _workContext = workContext;
            _userAcct = userAcct;
            _userFactory = userFactory;
            _userRole = userRole;
            _branchFactory = branchFactory;
            _regionFactory = regionFactory;
            _groupFactory = groupFactory;
            _linemanagerFactory = linemanagerFactory;
            _departmentFactory = departmentFactory;
            _unitOfWork = unitOfWork;
        }

        [Route("getbranches")]
        public IHttpActionResult GetBranches( IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var branch = _userFactory.ExecuteProcedure<BranchDto>("SP_GetTableBranch", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var obj = new SearchResponse<BranchDto>
            {
                draw = request.Draw,
                recordsTotal = branch.Count() > 0 ? branch[0].TotalRecords : 0,
                recordsFiltered = branch.Count() > 0 ? branch[0].TotalRecords : 0,
                data = branch
            };
            return Ok(obj);
        }

        [Route("getregions")]
        public IHttpActionResult GetRegion( IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var region = _userFactory.ExecuteProcedure<RegionDto>("SP_GetTableRegion", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var obj = new SearchResponse<RegionDto>
            {
                draw = request.Draw,
                recordsTotal = region.Count() > 0 ? region[0].TotalRecords : 0,
                recordsFiltered = region.Count() > 0 ? region[0].TotalRecords : 0,
                data = region
            };
            return Ok(obj);
        }

        [Route("getgroups")]
        public IHttpActionResult GetGroups( IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var group = _userFactory.ExecuteProcedure<GroupDto>("SP_GetTableGroup", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var obj = new SearchResponse<GroupDto>
            {
                draw = request.Draw,
                recordsTotal = group.Count() > 0 ? group[0].TotalRecords : 0,
                recordsFiltered = group.Count() > 0 ? group[0].TotalRecords : 0,
                data = group
            };
            return Ok(obj);
        }

        [Route("getdepts")]
        public IHttpActionResult GetDepts( IDataTablesRequest request )
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var dept = _userFactory.ExecuteProcedure<DeptDto>("SP_GetTableDepartmentList", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var obj = new SearchResponse<DeptDto>
            {
                draw = request.Draw,
                recordsTotal = dept.Count() > 0 ? dept[0].TotalRecords : 0,
                recordsFiltered = dept.Count() > 0 ? dept[0].TotalRecords : 0,
                data = dept
            };
            return Ok(obj);
        }
        [Route("getlinemanager")]
        public IHttpActionResult GetlineM( IDataTablesRequest request )
        {
            var line = _userFactory.ExecuteProcedure<LineManagerDto>("SP_GetLineManager", _workContext.User.OrganizationId, request.Search.Value, request.Start, request.Length).ToList();


            var obj = new SearchResponse<LineManagerDto>
            {
                draw = request.Draw,
                recordsTotal = line.Count() > 0 ? line[0].TotalRecords : 0,
                recordsFiltered = line.Count(),
                data = line
            };
            return Ok(obj);
        }

        [Route("getgroupdepts")]
        public IHttpActionResult getgroupdepts(IDataTablesRequest request )
        {
            var model = JsonConvert.DeserializeObject<GroupDto>(Convert.ToString(request.AdditionalParameters["data"]));
            var groupId = model.GroupId;
            var dept = _userFactory.ExecuteProcedure<DeptDto>("SP_GetTableDepartment", groupId,_workContext.User.OrganizationId, request.Search.Value, request.Start, request.Length).ToList();


            var obj = new SearchResponse<DeptDto>
            {
                draw = request.Draw,
                recordsTotal = dept.Count() > 0 ? dept[0].TotalRecords : 0,
                recordsFiltered = dept.Count() > 0 ? dept[0].TotalRecords : 0,
                data = dept
            };
            return Ok(obj);
        }

        [HttpPost]
        [Route("SaveNewBranch")]
        public IHttpActionResult SaveNewBranch( BranchDto model )
        {
            var response = new ApiResult<BranchDto>();
            if (ModelState.IsValid)
            {
                try
                {
                    //Add
                    var name = _branchFactory.GetIncluding(x => x.Name.ToUpper().Contains(model.BranchName.ToUpper()), false);
                    if (name == null)
                    {
                        var branch = new Branch();
                        branch.Name = model.BranchName;
                        branch.OrganizationId = _workContext.User.OrganizationId;
                        branch.CreatedById = _workContext.User.Id;
                        branch.CreatedDate = DateTime.Now;

                        _branchFactory.Add(branch);

                        response.HasError = false;
                        response.Message = "Branch successfully created";
                        var dto = new BranchDto();
                        dto.BranchId = branch.Id;
                        response.Result = dto;
                    }
                    else
                    {
                        response.HasError = true;
                        response.Message = "Branch already exists";
                    }
                }
                catch (Exception e)
                {

                    response.HasError = true;
                    response.Message = "" + e.Message;
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "Some data is invalid. Please check again";
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateBranch")]
        public IHttpActionResult UpdateBranch( BranchDto model )
        {
            var response = new ApiResult<string>();
            if (ModelState.IsValid)
            {
                //Update
                var br = _branchFactory.GetIncluding(x => x.Id == model.BranchId, true);
                if (br != null)
                {
                    br.Name = model.BranchName;
                    br.OrganizationId = _workContext.User.OrganizationId;
                    br.CreatedById = _workContext.User.Id;
                    br.CreatedDate = DateTime.Now;
                    br.LastModifiedById = _workContext.User.Id;
                    br.ModifiedDate = DateTime.Now;

                    _branchFactory.Update(br);
                    response.HasError = false;
                    response.Message = "Branch successfully updated";

                }
                else
                {
                    response.HasError = true;
                    response.Message = "Unable to update branch";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = "Some data is invalid. Check them.";
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getbranchupdate")]
        public IHttpActionResult getBrachId( int id )
        {
            var data = new ApiResult<BranchDto>();
            var branch = _branchFactory.GetIncluding(x => x.Id == id, false);
            if (branch != null)
            {
                var dto = new BranchDto();
                dto.BranchId = branch.Id;
                dto.BranchName = branch.Name;
                data.HasError = false;
                data.Result = dto;
            }
            else
            {
                data.HasError = true;
                data.Message = "Branch not found";
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("deletebranch")]
        public IHttpActionResult deletebranch(int id )
        {
            var data = new ApiResult<string>();
            var branch = _branchFactory.GetIncluding(x => x.Id == id, true);
            if (branch != null)
            {
                branch.IsDeleted = true;
                branch.LastModifiedById = _workContext.User.Id;
                branch.ModifiedDate = DateTime.Now;

                _branchFactory.Update(branch);
                data.HasError = false;
                data.Message = "Branch successfully deleted";

            }
            else
            {
                data.HasError = true;
                data.Message = "Unable to delete branch.";
            }
                return Ok(data);
        }

        [HttpPost]
        [Route("SaveNewRegion")]
        public IHttpActionResult SaveNewRegion(RegionDto model )
        {
            var response = new ApiResult<RegionDto>();
            if (ModelState.IsValid)
            {
                try
                {
                    //Add
                    var name = _regionFactory.GetIncluding(x => x.Name.ToUpper().Contains(model.RegionName.ToUpper()), false);
                    if (name == null)
                    {
                        var region = new Region();
                        region.Name = model.RegionName;
                        region.OrganizationId = _workContext.User.OrganizationId;
                        region.CreatedById = _workContext.User.Id;
                        region.CreatedDate = DateTime.Now;

                        _regionFactory.Add(region);

                        response.HasError = false;
                        response.Message = "Region successfully created";
                        
                    }
                    else
                    {
                        response.HasError = true;
                        response.Message = "Region already exists";
                    }
                }
                catch (Exception e)
                {

                    response.HasError = true;
                    response.Message = "" + e.Message;
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "Some data is invalid. Please check again";
            }
            return Ok(response);

        }

        [HttpGet]
        [Route("deleteregion")]
        public IHttpActionResult deleteregion( int id )
        {
            var data = new ApiResult<string>();
            var region = _regionFactory.GetIncluding(x => x.Id == id, true);
            if (region != null)
            {
                region.IsDeleted = true;
                region.LastModifiedById = _workContext.User.Id;
                region.ModifiedDate = DateTime.Now;

                _regionFactory.Update(region);
                data.HasError = false;
                data.Message = "Region successfully deleted";

            }
            else
            {
                data.HasError = true;
                data.Message = "Unable to delete region.";
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveNewGroup")]
        public IHttpActionResult SaveNewGroup(GroupDto model )
        {
            var response = new ApiResult<GroupDto>();
            if (ModelState.IsValid)
            {
                var grouphead = _unitOfWork.Repository<GroupDto>().SqlQuery("SELECT a.FirstName as GroupHeadFirstName, a.LastName as GroupHeadLastName, g.[Name] as GradeName, d.[Name] as DepartmentName FROM AspNetUsers a INNER JOIN Grade g on g.Id=a.GradeId INNER JOIN Department d on d.Id=a.DepartmentId WHERE a.StaffId=" + model.GroupHeadStaffId).FirstOrDefault();

                if (grouphead != null)
                {
                    try
                    {
                        //Add
                        var name = _groupFactory.GetIncluding(x => x.Name.ToUpper().Contains(model.GroupName.ToUpper()), false);
                        if (name == null)
                        {
                            var group = new Group();
                            group.Name = model.GroupName;
                            group.GroupHeadFirstName = grouphead.GroupHeadFirstName;
                            group.GroupHeadLastName = grouphead.GroupHeadLastName;
                            group.GroupHeadStaffId = model.GroupHeadStaffId;
                            group.OrganizationId = _workContext.User.OrganizationId;
                            group.CreatedById = _workContext.User.Id;
                            group.CreatedDate = DateTime.Now;

                            _groupFactory.Add(group);

                            response.HasError = false;
                            response.Message = "Group successfully created";
                            var g = new GroupDto();
                            g.GroupHeadFirstName = group.GroupHeadFirstName;
                            g.GroupHeadLastName = group.GroupHeadLastName;
                            g.GradeName = grouphead.GradeName;
                            response.Result = g;
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Group already exists";
                        }
                    }
                    catch (Exception e)
                    {

                        response.HasError = true;
                        response.Message = "" + e.Message;
                    }

                }
                else
                {
                    response.HasError = true;
                    response.Message = "Group head does not exists";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "Some data is invalid. Please check again";
            }
            return Ok(response);

        }
    }
}
