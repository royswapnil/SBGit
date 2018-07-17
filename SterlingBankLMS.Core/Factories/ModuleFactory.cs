using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class ModuleFactory : GenericService<Module>
    {
        private readonly ICacheManager _cacheManagerSvc;
        public readonly string COURSEMODULES = "coursemodules-{0}-{1}";

        public ModuleFactory(IUnitOfWork unitOfWork, ICacheManager cacheManagerSvc) : base(unitOfWork)
        {
            _cacheManagerSvc = cacheManagerSvc;
        }

        public IEnumerable<CourseModuleNameDto> GetCourseModulesAndLessonsDto(int courseId, int orgId)
        {
            var courseModules = GetAllIncluding(x => x.CourseId == courseId
                                                   && !x.IsDeleted && x.Course.OrganizationId == orgId,
                                                   false, l => l.Lessons.Where(le => !le.IsDeleted).OrderBy(o => o.SortOrder))
                                                .OrderBy(x => x.SortOrder).ToList();

            return courseModules.Select(m => new CourseModuleNameDto
            {
                Name = m.Name,
                SortOrder = m.SortOrder,
                Id = m.Id,
                Lessons = m.Lessons.Select(l => new LessonNameDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Type = l.LessonContentType,
                    ModuleId = m.Id
                })
            });
        }

        public IEnumerable<CompletedLessonDto> GetAllCompletedLessons(int usercourseId)
        {
            return ExecuteProcedure<CompletedLessonDto>("Sp_GetUserCourseCompletedLessons", usercourseId).ToList();
        }

        //Todo (Samuel): Performance check on this code
        public IEnumerable<Module> GetClaModulesForCourse(int courseId, int orgId)
        {
            string key = string.Format(COURSEMODULES, courseId, orgId);

            var modules = _cacheManagerSvc.Get(key, () => {

                return GetAllIncluding(m => m.CourseId == courseId && !m.IsDeleted, false,
                                x => x.Lessons.Where(l => !l.IsDeleted)
                                .OrderBy(ob => ob.SortOrder), x => x.Course)

                .OrderBy(x => x.SortOrder)
                .ToList();
            });

            return modules;
        }

        public IEnumerable<LessonNameDto> GetContentsForModule(int moduleId)
        {
            var query = from q in UnitOfWork.Repository<Lesson>().TableNoTracking
                        where q.ModuleId == moduleId && !q.IsDeleted
                        orderby q.SortOrder
                        select new LessonNameDto { Id = q.Id, Name = q.Name, ModuleId = q.ModuleId };

            return query.ToList();
        }

        public void DeleteModule(Module entity, int userID)
        {
            this.UnitOfWork.BeginTransaction();
            try {
                foreach (var lesson in entity.Lessons) {
                    lesson.IsDeleted = true;
                    lesson.LastModifiedById = userID;
                    lesson.ModifiedDate = AppHelper.GetCurrentDate();
                }

                entity.IsDeleted = true;
                entity.LastModifiedById = userID;
                entity.ModifiedDate = AppHelper.GetCurrentDate();
                this.Update(entity);
                this.UnitOfWork.Commit();
            }
            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                throw;
            }

        }

        public bool ResortModule(List<Module> modelModule, List<Module> savedModule, int UserID)
        {

            this.UnitOfWork.BeginTransaction();
            try {
                foreach (var module in modelModule) {
                    var savedModuleItem = savedModule.Where(x => x.Id == module.Id).FirstOrDefault();
                    if (savedModuleItem != null) {
                        if (module.SortOrder != 0) {
                            savedModuleItem.SortOrder = module.SortOrder;
                            savedModuleItem.ModifiedDate = AppHelper.GetCurrentDate();
                            savedModuleItem.LastModifiedById = UserID;
                        }

                        foreach (var lesson in module.Lessons) {
                            var savedLessonItem = savedModuleItem.Lessons.Where(x => x.Id == lesson.Id).FirstOrDefault();
                            savedLessonItem.SortOrder = lesson.SortOrder;
                            savedLessonItem.ModifiedDate = AppHelper.GetCurrentDate();
                            savedLessonItem.LastModifiedById = UserID;

                        }
                    }


                }

                this.UnitOfWork.SaveChanges();

                this.UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex) {
                this.UnitOfWork.Rollback();
                return false;
            }

        }


        public int GetModuleMaxSort(int courseId)
        {

            var _context = this.All(x => x.CourseId == courseId && !x.IsDeleted, false)
                .OrderByDescending(x => x.SortOrder).Take(1).FirstOrDefault();

            return (_context == null) ? 0 : _context.SortOrder;
        }
    }
}
