using System.Web.Http;

namespace SterlingBankLMS.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/course")]
    public class CourseApiController : ApiController
    {
    //    private readonly IBaseService<Course> _courseService;
    //    public CourseApiController( IBaseService<Course> courseService )
    //    {
    //        _courseService = courseService;
    //    }
    //    [Route("getcourses")]
    //    [HttpGet]
    //    public HttpResponseMessage GetCourses()
    //    {
    //        System.Threading.Thread.Sleep(3000);
    //        ApiResult<List<CourseViewModel>> response = new ApiResult<List<CourseViewModel>>
    //        {
    //            HasError = true
    //        };
    //        var courseViewModels = new List<CourseViewModel> { };
    //        try
    //        {
    //            var courses = _courseService.GetAllIncluding(p => !p.IsDeleted, false);
    //            foreach (var course in courses)
    //            {
    //                var c = new CourseViewModel { };
    //                c.CourseName = course.CourseName;
    //                c.CourseDescription = course.CourseDescription;
    //                c.CourseRetakeCount = course.CourseRetakeCount;
    //                c.Id = course.Id;
    //                c.CreatedDate = course.CreatedDate;
    //                c.ImageUrl = course.ImageUrl;
    //                c.DueDate = course.DueDate;
    //                courseViewModels.Add(c);
    //            }
    //            response.Result = courseViewModels;
    //            response.HasError = false;
    //            return Request.CreateResponse(HttpStatusCode.OK, response);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.ErrorMessage = ex.Message;
    //            response.HasError = true;
    //            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
    //        }
    //    }


    //    [Route("getcourseinfo")]
    //    [HttpGet]
    //    public HttpResponseMessage GetCourseDetail( int id )
    //    {
    //        //System.Threading.Thread.Sleep(3000);
    //        ApiResult<CourseViewModel> response = new ApiResult<CourseViewModel>
    //        {
    //            HasError = true
    //        };
    //        var cv = new CourseViewModel();

    //        try
    //        {
    //            Course courses = _courseService.GetCourse(id);
    //            if (courses != null)
    //            {
    //                cv.CourseDescription = courses.CourseDescription;
    //                cv.CourseName = courses.CourseName;
    //                cv.Id = courses.Id;
    //                cv.ImageUrl = courses.ImageUrl;
    //                cv.CreatedDate = courses.CreatedDate;
    //                cv.CourseRetakeCount = courses.CourseRetakeCount;
    //                cv.DueDate = courses.DueDate;
    //                cv.TimeDuration = CourseService.TimeFuture(courses.DueDate, courses.CreatedDate);
    //                cv.Modules = courses.Modules.OrderBy(x => x.HierarchicalOrder).Select(x => new ModuleViewModel
    //                {
    //                    ModuleName = x.ModuleName,
    //                    Id = x.Id,
    //                    CourseId = x.CourseId,
    //                    Topics = x.Topics.Select(y => new TopicViewModel
    //                    {
    //                        ContentUrl = y.ContentUrl,
    //                        Id = y.Id,
    //                        ModuleId = y.ModuleId,
    //                        TopicName = y.TopicName,
    //                        ContentType = Convert.ToInt32(y.CourseType) == 1 ? "Text" : "Video"
    //                    })
    //                });
    //                response.Result = cv;
    //                response.HasError = false;
    //                response.ErrorMessage = "";
    //                response.Message = "Course details successfully retrieved";

    //                return Request.CreateResponse(HttpStatusCode.OK, response);

    //            }
    //            else
    //            {
    //                response.Result = null;
    //                response.HasError = true;
    //                response.ErrorMessage = "Course not found";
    //                response.Message = "";
    //                return Request.CreateResponse(HttpStatusCode.NotFound, response);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            response.HasError = true;
    //            response.ErrorMessage = ex.Message;
    //            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
    //        }

    //    }

    //    [Route("getallvideos")]
    //    [HttpGet]
    //    public HttpResponseMessage GetFirstModule( int id )
    //    {
    //        //System.Threading.Thread.Sleep(3000);
    //        ApiResult<CourseViewModel> response = new ApiResult<CourseViewModel>
    //        {
    //            HasError = true
    //        };
    //        var cv = new CourseViewModel();

    //        try
    //        {
    //            Course courses = _courseService.GetCourse(id);
    //            if (courses != null)
    //            {
    //                cv.CourseName = courses.CourseName;
    //                cv.Modules = courses.Modules.OrderBy(x => x.HierarchicalOrder).Select(x => new ModuleViewModel
    //                {
    //                    ModuleName = x.ModuleName,

    //                    HierachicalOrder = x.HierarchicalOrder,
    //                    Topics = x.Topics.Select(y => new TopicViewModel
    //                    {
    //                        ContentUrl = y.ContentUrl,
    //                        TopicName = y.TopicName,
    //                        ContentType = Convert.ToInt32(y.CourseType) == 1 ? "Text" : "Video"
    //                    })
    //                });
    //                response.Result = cv;
    //                response.HasError = false;
    //                response.ErrorMessage = "";
    //                response.Message = "Course details successfully retrieved";

    //                return Request.CreateResponse(HttpStatusCode.OK, response);

    //            }
    //            else
    //            {
    //                response.Result = null;
    //                response.HasError = true;
    //                response.ErrorMessage = "Course not found";
    //                response.Message = "";
    //                return Request.CreateResponse(HttpStatusCode.NotFound, response);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            response.HasError = true;
    //            response.ErrorMessage = ex.Message;
    //            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
    //        }

    //    }

    //    [Route("gettopic")]
    //    [HttpGet]
    //    public HttpResponseMessage GetFirstTopic( int id )
    //    {
    //        //System.Threading.Thread.Sleep(3000);
    //        ApiResult<CourseViewModel> response = new ApiResult<CourseViewModel>
    //        {
    //            HasError = true
    //        };
    //        var cv = new CourseViewModel();

    //        try
    //        {
    //            Course courses = _courseService.GetCourse(id);
    //            if (courses != null)
    //            {
    //                cv.CourseDescription = courses.CourseDescription;
    //                cv.CourseName = courses.CourseName;
    //                cv.Id = courses.Id;
    //                cv.Modules = courses.Modules.OrderBy(x => x.HierarchicalOrder).Select(x => new ModuleViewModel
    //                {
    //                    ModuleName = x.ModuleName,
    //                    HierachicalOrder = x.HierarchicalOrder,
    //                    Topics = x.Topics.OrderBy(c => c.Module.HierarchicalOrder).Select(y => new TopicViewModel
    //                    {
    //                        ContentUrl = y.ContentUrl,
    //                        TopicName = y.TopicName,
    //                        ContentType = Convert.ToInt32(y.CourseType) == 1 ? "Text" : "Video"
    //                    })
    //                });
    //                response.Result = cv;
    //                response.HasError = false;
    //                response.ErrorMessage = "";
    //                response.Message = "Course details successfully retrieved";

    //                return Request.CreateResponse(HttpStatusCode.OK, response);

    //            }
    //            else
    //            {
    //                response.Result = null;
    //                response.HasError = true;
    //                response.ErrorMessage = "Course not found";
    //                response.Message = "";
    //                return Request.CreateResponse(HttpStatusCode.NotFound, response);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            response.HasError = true;
    //            response.ErrorMessage = ex.Message;
    //            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
    //        }

      }
    }
