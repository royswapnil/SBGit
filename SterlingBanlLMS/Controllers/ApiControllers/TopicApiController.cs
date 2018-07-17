namespace SterlingBankLMS.Web.Controllers.ApiControllers
{
    //[RoutePrefix("api/topic")]
    //public class TopicApiController : ApiController
    //{
    //    private readonly IBaseService<Topic> _topicService;

    //    public TopicApiController( IBaseService<Topic> topicService )
    //    {
    //        _topicService = topicService;
    //    }

    //    [Route("getvideos")]
    //    [HttpGet]
    //    public HttpResponseMessage GetVideos()
    //    {
    //        ApiResult<List<TopicViewModel>> response = new ApiResult<List<TopicViewModel>>();

    //        var list = new List<TopicViewModel>();
    //        try
    //        {
    //            IEnumerable<Topic> videos = _topicService.GetVideos();

    //            foreach (var video in videos)
    //            {
    //                list.Add(new TopicViewModel
    //                {
    //                    ContentUrl = video.ContentUrl,
    //                    ContentType = Convert.ToInt32(video.CourseType) == 1 ? "Text" : "Video",
    //                    TopicName = video.TopicName,
    //                    DueDate = video.Module.EndDate
    //                });
    //            }
    //            response.ErrorMessage = null;
    //            response.HasError = false;
    //            response.Message = "Videos successfully retrieved";
    //            response.Result = list;

    //            return Request.CreateResponse(HttpStatusCode.OK, response);
    //        }
    //        catch (Exception ex)
    //        {

    //            response.ErrorMessage = ex.Message;
    //            response.Message = "Server Error";
    //            response.HasError = true;
    //            response.Result = null;

    //            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
    //        }

    //    }
    //}
}
