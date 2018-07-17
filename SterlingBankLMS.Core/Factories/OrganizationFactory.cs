using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class OrganizationFactory : GenericService<Organization>
    {
        public OrganizationFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {

        }

        public List<OrganizationDto> GetAllOrganization( int pageSize, int pageNumber, string search, out int TotalRecords )
        {
            var _organ = UnitOfWork.Repository<Organization>().TableNoTracking;

            var query = (from x in _organ
                         where !x.IsDeleted
                         && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && x.Name.Contains(search)))
                         orderby x.Id
                         select new OrganizationDto
                         {
                             OrganizationalStatus = x.OrganizationalStatus,
                             OrganizationId = x.Id,
                             OrganizationName = x.Name,
                             CreatedDate = x.CreatedDate,
                             Email = x.Email,
                             LogoUrl = x.LogoUrl,
                             SubDomain = x.SubDomain,

                         }).Skip(pageNumber * pageSize).Take(pageSize).Future();


            var queryCount = (from a in _organ
                              where !a.IsDeleted
                              select a).DeferredCount().FutureValue();

            TotalRecords = queryCount.Value;

            var ReturnList = query.ToList();

            return ReturnList;
        }

        public List<CourseDto> GetOrganizationCourses( int organizationId, int pageSize, int pageNumber, string search, out int TotalRecords )
        {
            var _course = UnitOfWork.Repository<Course>().TableNoTracking;

            var query = (from x in _course.Include(y => y.LearningArea)
                         where !x.IsDeleted && x.OrganizationId == organizationId
                         && (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && x.Name.Contains(search)))
                         orderby x.Id
                         select new CourseDto
                         {
                             Name = x.Name,
                             LearningAreaName = x.LearningArea.Name,
                             CreatedDate = x.CreatedDate,
                             ImageUrl = x.ImageUrl,
                             IsPublished = x.IsPublished,
                             //PassGrade = x.PassGrade,
                             Description = x.Description,
                             DueDate = x.DueDate,
                             Id = x.Id

                         }).Skip(pageNumber * pageSize).Take(pageSize).Future();


            var queryCount = (from a in _course
                              where !a.IsDeleted
                              select a).DeferredCount().FutureValue();

            TotalRecords = queryCount.Value;

            var ReturnList = query.ToList();

            return ReturnList;

        }
    }
}