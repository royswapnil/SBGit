using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class LearningGroupFactory : GenericService<LearningGroup>
    {
        public LearningGroupFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public LearningGroup AddorUpdateLearningGroup(LearningGroup learningGroup, int userId, int orgId)
        {
            var saveGroup = learningGroup.Id == 0 ? learningGroup : GetIncluding(x => x.Id == learningGroup.Id, true, x => x.Tags);

            if (saveGroup == null)
                throw new ArgumentNullException();

            this.UnitOfWork.BeginTransaction();
            try
            {
                if (learningGroup.Id == 0)
                {
                    saveGroup.CreatedDate = AppHelper.GetCurrentDate();
                    saveGroup.OrganizationId = orgId;
                    saveGroup.CreatedById = userId;

                    foreach (var tag in saveGroup.Tags)
                    {
                        tag.CreatedDate = saveGroup.CreatedDate;
                        tag.ModifiedDate = saveGroup.ModifiedDate;
                    }
                }

                else
                {
                    saveGroup.Name = learningGroup.Name;
                    saveGroup.TagFormat = learningGroup.TagFormat;
                    /// get deleted tags
                    /// 

                    foreach (var tag in saveGroup.Tags)
                    {
                        if (!learningGroup.Tags.Any(x => x.TagType == tag.TagType && x.TagValue == tag.TagValue))
                        {
                            tag.IsDeleted = true;
                            tag.ModifiedDate = saveGroup.ModifiedDate;
                        }
                    }

                    /// get added tags
                    /// 

                    foreach (var tag in learningGroup.Tags)
                    {
                        if (!saveGroup.Tags.Any(x => x.TagType == tag.TagType && x.TagValue == tag.TagValue))
                        {
                            tag.CreatedDate = saveGroup.CreatedDate;
                            tag.ModifiedDate = saveGroup.ModifiedDate;
                            saveGroup.Tags.Add(tag);
                        }
                    }


                }

                saveGroup.ModifiedDate = learningGroup.CreatedDate;
                saveGroup.LastModifiedById = userId;

                if (saveGroup.Id == 0)
                    Add(saveGroup);
                else
                    Update(saveGroup);

                this.UnitOfWork.Commit();

                return saveGroup;

            }

            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }
            
        }

        public bool ValidateNameExists(string Name, int Id)
        {
            var existing = Find(x => x.Name == Name && x.Id != Id, false);
            return (existing == null);
        }


        public IEnumerable<LearningGroupDto> GetLearningGroupTable(string search, int pageIndex, int pageSize, int organizationId, out int TotalRecords)
        {

            var query = ExecuteProcedure<LearningGroupDto>("Sp_GetLearningGroupList", organizationId, search, pageIndex, pageSize).ToList();
            TotalRecords = query.Count() > 0 ? query.First().TotalRecords : 0;

            return query;
        }

        /// <summary>
        /// Assign new courses to learning group
        /// </summary>
        /// <param name="LearningGroupCourse"></param>
        /// <param name="UserID"></param>
        public void AssignLearningGroupCourses(List<LearningGroupCourse> LearningGroupCourse, int UserID)
        {
            this.UnitOfWork.BeginTransaction();
            try
            {
                var LearningGroupId = LearningGroupCourse[0].LearningGroupId;
                var _context = this.GetContext().Set<LearningGroupCourse>();
                var existingCourses = _context.Where(x => x.LearningGroupId == LearningGroupId).ToList();

                /// loop through courses if new add, if old ignore
                foreach (var item in LearningGroupCourse)
                {
                    if (!existingCourses.Any(x => x.CourseId == item.CourseId))
                    {
                        item.CreatedDate = AppHelper.GetCurrentDate();
                        item.ModifiedDate = item.CreatedDate;
                        item.CreatedById = UserID;
                        item.LastModifiedById = UserID;
                        _context.Add(item);
                    }

                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }
        }


        public void AssignTrainingGroupTrainings(List<LearningGroupTraining> training, int UserID,int OrganizationId)
        {

            var TrainingGroupId = training[0].LearningGroupId;
            var assigned = string.Join(",", training.Select(x => x.TrainingId));
            ExecuteProcedure<LearningGroupTraining>("Sp_AssignTraining", TrainingGroupId, assigned, UserID, OrganizationId);
            
        }

        /// <summary>
        /// Assign survey group surveys
        /// </summary>
        /// <param name="SurveyGroupSurveys"></param>
        /// <param name="UserID"></param>

        public void AssignSurveyGroupSurveys(List<LearningGroupSurvey> SurveyGroupSurveys, int UserID,int OrganizationId)
        {
            var SurveyGroupId = SurveyGroupSurveys[0].LearningGroupId;
            var assigned = string.Join(",", SurveyGroupSurveys.Select(x => x.SurveyId));
            ExecuteProcedure<LearningGroupTraining>("Sp_AssignSurvey", SurveyGroupId, assigned, UserID, OrganizationId);

        }


        /// <summary>
        /// Assign exam group exams
        /// </summary>
        /// <param name="ExamGroupExam"></param>
        /// <param name="UserID"></param>

        public void AssignExamGroupExams(List<LearningGroupExam> ExamGroupExams, int UserID,int OrganizationId)
        {
            var ExamGroupId = ExamGroupExams[0].LearningGroupId;
            var assigned = string.Join(",", ExamGroupExams.Select(x => x.ExaminationId));
            ExecuteProcedure<LearningGroupExam>("Sp_AssignExamination", ExamGroupId, assigned, UserID, OrganizationId);
        }


        /// <summary>
        /// Get Users that fall into selected learning group tags
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="organizationId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>

        public List<EmployeeDetailsDto> GetUsersByLearningGroupTag(List<LearningGroupTag> tags, int organizationId, int pageSize, int pageNumber, string search)
        {
            //// group tags according to search criterias 

            var offset = pageSize * pageNumber;

            var OrgGroupGroup = tags.Where(x => x.TagType == LearningGroupTagType.Group);
            var GenderGroup = tags.Where(x => x.TagType == LearningGroupTagType.Gender);
            var GradeGroup = tags.Where(x => x.TagType == LearningGroupTagType.Grade);
            var RoleGroup = tags.Where(x => x.TagType == LearningGroupTagType.Role);
            var IndividualGroup = tags.Where(x => x.TagType == LearningGroupTagType.Individual);
            var RegionGroup = tags.Where(x => x.TagType == LearningGroupTagType.Region);
            var DepartmentGroup = tags.Where(x => x.TagType == LearningGroupTagType.Department);

            var IsOnlyIndividual = true;

            if (OrgGroupGroup.Count() > 0 || GenderGroup.Count() > 0 || GradeGroup.Count() > 0 || RoleGroup.Count() > 0
                || RegionGroup.Count() > 0 || DepartmentGroup.Count() > 0)
            {
                IsOnlyIndividual = false;
            }

            ////// build query based on groups

            var _context = this.GetContext().Set<LearningGroupTag>();
            var sql = "select {0} from AspnetUsers a {1}";

            var joinSql = new StringBuilder();
            var whereSql = new StringBuilder();
            var unionSql = new StringBuilder();
            string roleJoin = "";
            string searchSql = "";

            joinSql.Append(" left join Department b on a.DepartmentId = b.Id ");
            joinSql.Append(" left join Branch c on a.BranchId = c.Id ");
            joinSql.Append(" left join Region d on a.RegionId = d.Id ");
            joinSql.Append(" left join grade e on a.GradeId = e.Id ");

            if (RoleGroup.Count() > 0)
            {
                roleJoin = " inner join AspNetUserRoles roles on a.Id = roles.UserId ";
                joinSql.Append(roleJoin);
            }



            whereSql.Append(" where ( (a.OrganizationId = " + organizationId + ") and ");

            if (!string.IsNullOrEmpty(search))
                searchSql = " and ((a.FirstName like '%" + search + "%') or (a.LastName like '%" + search + "%' ) or (b.Name like '%" + search + "%' ) or (c.Name like '%" + search + "%' ) or (d.Name like '%" + search + "%' ) or (e.Name like '%" + search + "%' ))";

            bool whereItem = false;

            foreach (var item in OrgGroupGroup)
            {
                if (whereItem)
                    whereSql.Append(" or ");
                whereSql.Append("(a.GroupId = " + item.TagValue + ")");
                whereItem = true;
            }

            if (GenderGroup.Count() > 0)
            {
                if (whereItem)
                {
                    whereSql.Append(") and (");
                    whereItem = true;
                }
                foreach (var item in GenderGroup)
                {
                    whereSql.Append("(a.Gender = " + item.TagValue + ")");
                }

            }


            if (GradeGroup.Count() > 0)
            {
                if (whereItem)
                {
                    whereSql.Append(") and (");
                    whereItem = true;
                }
                bool condtionItem = false;
                foreach (var item in GradeGroup)
                {
                    if (condtionItem)
                        whereSql.Append(" or ");
                    whereSql.Append("(a.GradeId = " + item.TagValue + ")");
                    condtionItem = true;
                }

            }


            if (RegionGroup.Count() > 0)
            {
                if (whereItem)
                {
                    whereSql.Append(") and (");
                    whereItem = true;
                }
                bool condtionItem = false;
                foreach (var item in RegionGroup)
                {
                    if (condtionItem)
                        whereSql.Append(" or ");
                    whereSql.Append("(a.RegionId = " + item.TagValue + ")");
                    condtionItem = true;
                }
            }

            if (DepartmentGroup.Count() > 0)
            {
                if (whereItem)
                {
                    whereSql.Append(") and (");
                    whereItem = true;
                }
                bool condtionItem = false;

                foreach (var item in DepartmentGroup)
                {
                    if (condtionItem)
                        whereSql.Append(" or ");
                    whereSql.Append("(a.DepartmentId = " + item.TagValue + ")");

                    condtionItem = true;
                }
            }

            if (RoleGroup.Count() > 0)
            {
                if (whereItem)
                    whereSql.Append(") and (");
                bool condtionItem = false;
                foreach (var item in RoleGroup)
                {
                    if (condtionItem)
                        whereSql.Append(" or ");
                    whereSql.Append("(roles.RoleId = " + item.TagValue + ")");

                    condtionItem = true;
                }
            }

            var unionWhere = new StringBuilder();
            var countUnion = "";
            if (IndividualGroup.Count() > 0)
            {
                unionSql.Append((IsOnlyIndividual ? "" : "union all") + " select @TotalRecords TotalRecords, a.Id, a.StaffId, a.FirstName, a.LastName, b.Name as Department, c.Name as Branch, d.Name as Location, a.Gender, e.Name as Grade from aspnetusers a ");
                bool condtionItem = false;
                unionSql.Append(joinSql.ToString());
                unionSql.Append("where ");
                foreach (var item in IndividualGroup)
                {
                    if (condtionItem)
                        unionWhere.Append(" or ");
                    unionWhere.Append("(a.Id = " + item.TagValue + ")");

                    condtionItem = true;
                }
                unionSql.Append(unionWhere.ToString());
                countUnion = (IsOnlyIndividual ? "" : "union all") + " select a.Id from aspnetusers a where " + unionWhere.ToString();
            }

            var countSql = "";
            var dataSql = "";

            if (IsOnlyIndividual)
            {
                countSql = "declare @TotalRecords int = (" + countUnion + ")";
                dataSql = unionSql.ToString();
            }
            else
            {
                countSql = "declare @TotalRecords int = (select count(t1.Id) from ( " + string.Format(sql, "a.id ", roleJoin + whereSql.ToString() + ")" + countUnion + ") t1)");
                dataSql = string.Format(sql.ToString(),
                   "@TotalRecords TotalRecords,a.Id,a.StaffId,a.FirstName,a.LastName,b.Name as Department,c.Name as Branch,d.Name as Location,a.Gender,e.Name as Grade ",
                   joinSql.ToString() + whereSql.ToString() + searchSql + ")" + unionSql.ToString());
            }


            var mysql = new StringBuilder();
            mysql.Append(countSql);
            mysql.Append(dataSql);
            mysql.Append(" ORDER  BY a.Id OFFSET(" + offset + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY");

            return this.UnitOfWork.Repository<EmployeeDetailsDto>().SqlQuery(mysql.ToString(), "").ToList();
        }

        /// <summary>
        /// Get Users by Learning Group ID
        /// </summary>
        /// <param name="LearningGroupId"></param>
        /// <returns></returns>
        public List<LearningGroupUserDto> GetUsersByLearningGroupID(int LearningGroupId,int organizationId, string search, int pageNumber, int? pageSize)
        {
            return ExecuteProcedure<LearningGroupUserDto>("Sp_GetUsersByLearningGroupID", LearningGroupId, search, pageNumber, pageSize,organizationId).ToList();

        }

        public LearningGroupDto GetLearningGroupById(int LearningGroupId)
        {
            return ExecuteProcedure<LearningGroupDto>("Sp_GetLearningGroupById", LearningGroupId).FirstOrDefault();

        }

        public void DeleteLearningGroup(int LearningGroupId, int userId)
        {
            var datenow = AppHelper.GetCurrentDate();
            var group = GetIncluding(x => x.Id == LearningGroupId, true, x => x.Tags);

            this.UnitOfWork.BeginTransaction();
            try
            {
                group.IsDeleted = true;
                group.LastModifiedById = userId;
                group.ModifiedDate = datenow;

                /// loop through surveys if new add, if old ignore
                foreach (var item in group.Tags)
                {
                    item.IsDeleted = true;
                    item.ModifiedDate = datenow;

                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback();
                throw;
            }

        }


    }
}
