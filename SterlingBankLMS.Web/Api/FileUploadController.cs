using ExcelManager;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.ExcelModels;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Enums;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using FastMember;
using System.Data;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Handle Api File Uploads from either chunked files or full files sent from client
    /// Saves to appropriate organization path via OrgID and file type sent
    /// </summary>
    [RoutePrefix("api/FileUpload")]
    public class FileUploadController : BaseApiController
    {
        private readonly FileUploader _fileUploader;
        private readonly IWorkContext _workContext;
        private readonly List<string> AllowedExts = AppConstants.AllowedFileExtensions;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly IPermissionService _permissionSvc;


        public FileUploadController(FileUploader fileUploader, IWorkContext workContext, NotificationHubFactory notificationHubFactory, IPermissionService permissionSvc)
        {
            _fileUploader = fileUploader;
            _workContext = workContext;
            _notificationHubFactory = notificationHubFactory;
            _permissionSvc = permissionSvc;
        }

        [HttpPost]
        [Route("UploadFile")]
        public IHttpActionResult UploadFile()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<string>();
            var Request = HttpContext.Current.Request;
            foreach (string file in Request.Files)
            {
                var FileDataContent = Request.Files[file];
                bool IsChunked = Convert.ToBoolean(Request["IsChunked"]);

                if (!IsChunked)
                {
                    if (!AllowedExts.Any(x => x == Path.GetExtension(FileDataContent.FileName).Remove(0, 1).ToLower()))
                    {
                        return BadRequest("File type not allowed");
                    }

                    string mimeType = MimeMapping.GetMimeMapping(FileDataContent.FileName);
                    string FileUploadPath;
                    if (mimeType.ToLower().Contains("video"))
                    {
                        FileUploadPath = AppConstants.VideoUploadPath;
                    }
                    else if (mimeType.ToLower().Contains("sheet"))
                    {
                        FileUploadPath = AppConstants.excelUploadPath;
                    }
                    else
                    {
                        FileUploadPath = AppConstants.DocUploadPath;
                    }


                    FileUploadPath = FileUploadPath + (int)_workContext.User.OrganizationId + "/";
                    var saveFilePath = _fileUploader.UploadFile(FileDataContent, FileUploadPath);
                    if (saveFilePath != null)
                    {
                        result.HasError = false;
                        result.Message = "File successfully uploaded";
                        result.Result = saveFilePath;
                    }
                    else
                    {
                        return BadRequest("An error occurred while saving file");
                    }
                }

                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    // take the input stream, and save it to a temp folder using
                    // the original file.part name posted

                    var FileUploadPath = AppConstants.FileChunkPath;
                    var stream = FileDataContent.InputStream;
                    var fileName = Path.GetFileName(FileDataContent.FileName);
                    string uploadPath = HostingEnvironment.MapPath(FileUploadPath);
                    string mergePath = Path.Combine(uploadPath, fileName);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    try
                    {
                        if (System.IO.File.Exists(mergePath))
                            System.IO.File.Delete(mergePath);
                        using (var fileStream = System.IO.File.Create(mergePath))
                        {
                            stream.CopyTo(fileStream);
                        }

                        // Once the file part is saved, see if we have enough to merge it
                        var updatedFileName = _fileUploader.MergeFile(mergePath);
                        if (updatedFileName == "invalid")
                        {
                            return BadRequest("File type not allowed");
                        }
                        result.HasError = false;
                        result.Result = updatedFileName;
                    }
                    catch (IOException ex)
                    {
                        throw;
                    }


                }
            }
            return Ok(result);

        }

        /// <summary>
        /// Bulk Course Upload 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>

        private bool ProcessBulk(string fileFullPath,int proccessId)
        {
          
            int UserId = _workContext.User.Id;
            int OrganizationId = _workContext.User.OrganizationId;

            Dictionary<string, object> processParams = new Dictionary<string, object>();
            processParams["Value"] = proccessId;
            processParams["Name"] = "ProcessId";

           
                var learningAreaExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.LearningArea>(0, false, null, processParams);
                var courseExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.Course>(1, false, null, processParams);
                var moduleExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.Module>(2, false, null, processParams);
                var lessonExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.Lesson>(3, false, null, processParams);
                var quizExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.QuizQuestions>(4, false, null, processParams);
                var optionExcel = new ExcelReader(fileFullPath).ReadWorkBookSheets<CourseExcelModel.QuizQuestionOption>(5, false, null, processParams);

                string constr = ConfigurationManager.ConnectionStrings["SterlingBankDbContext"].ConnectionString.ToString();


                var learnParams = learningAreaExcel.FirstOrDefault().GetType().GetProperties()
                   .Select(p => p.Name).ToList().ToArray();

                var courseParams = courseExcel.FirstOrDefault().GetType().GetProperties()
                 .Select(p => p.Name).ToList().ToArray();

                var moduleParams = moduleExcel.FirstOrDefault().GetType().GetProperties()
                .Select(p => p.Name).ToList().ToArray();

                var lessonParams = lessonExcel.FirstOrDefault().GetType().GetProperties()
                .Select(p => p.Name).ToList().ToArray();

                var quizParams = quizExcel.FirstOrDefault().GetType().GetProperties()
                .Select(p => p.Name).ToList().ToArray();

                var optionsParams = optionExcel.FirstOrDefault().GetType().GetProperties()
               .Select(p => p.Name).ToList().ToArray();


                using (var conn = new SqlConnection(constr))
                {
                    conn.Open();
                    using (var sqlCopy = new SqlBulkCopy(constr))
                    {
                        sqlCopy.DestinationTableName = "[UploadLearningArea]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(learningAreaExcel, learnParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }

                        sqlCopy.DestinationTableName = "[UploadCourse]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(courseExcel, courseParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }

                        sqlCopy.DestinationTableName = "[UploadModule]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(moduleExcel, moduleParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }

                        sqlCopy.DestinationTableName = "[UploadLesson]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(lessonExcel, lessonParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }

                        sqlCopy.DestinationTableName = "[UploadQuizQuestion]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(quizExcel, quizParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }

                        sqlCopy.DestinationTableName = "[UploadQuizQuestionOption]";
                        sqlCopy.BatchSize = 500;
                        using (var reader = ObjectReader.Create(optionExcel, optionsParams))
                        {
                            sqlCopy.WriteToServer(reader);
                        }


                        /// excute stored procedure to process items

                        SqlCommand cmd = new SqlCommand("SP_ProcessCourseBulkUpload");

                        cmd.Parameters.Add(new SqlParameter("@ProcessID", proccessId));
                        cmd.Parameters.Add(new SqlParameter("@UserID", UserId));
                        cmd.Parameters.Add(new SqlParameter("@OrganizationID", OrganizationId));
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                return true;
           
        }

        public IHttpActionResult UpdateBulkUploadNotification(BulkUploadModel upload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error occurred");
            }

            string fileFullPath = CommonHelper.MapPath(upload.FilePath, true);
            var result = new ApiResult<bool>();

            try
            {
                var notification = new NotificationHub
                {
                    CreatedById = _workContext.User.Id,
                    LastModifiedById = _workContext.User.Id,
                    CreatedDate = CommonHelper.GetCurrentDate(),
                    ModifiedDate = CommonHelper.GetCurrentDate(),
                    Message = "New course bulk upload started. You will be alerted once this process is completed",
                    NotificationType = NotificationType.CourseBulkUpdateEnd,
                    OrganizationId = _workContext.User.Id,
                    ReceiverId = _workContext.User.Id,
                    ActionURL = upload.FilePath
                };

                _notificationHubFactory.Add(notification);

                /// will be moved to hangfire
                if (ProcessBulk(fileFullPath, notification.Id))
                {
                    var passNotification = new NotificationHub
                    {
                        CreatedById = _workContext.User.Id,
                        LastModifiedById = _workContext.User.Id,
                        CreatedDate = CommonHelper.GetCurrentDate(),
                        ModifiedDate = CommonHelper.GetCurrentDate(),
                        Message = "Your course bulk update was successfull, Please validate entries on course page",
                        NotificationType = NotificationType.CourseBulkUpdateEnd,
                        OrganizationId = _workContext.User.Id,
                        ReceiverId = _workContext.User.Id
                    };

                    _notificationHubFactory.Add(notification);
                    result.HasError = true;
                    result.Message = "Upload successfull";
                }
                else
                {
                    var passNotification = new NotificationHub
                    {
                        CreatedById = _workContext.User.Id,
                        LastModifiedById = _workContext.User.Id,
                        CreatedDate = CommonHelper.GetCurrentDate(),
                        ModifiedDate = CommonHelper.GetCurrentDate(),
                        Message = "An error occurred when uploading your course data. All saved records have been reversed. Please confirm data validity and retry",
                        NotificationType = NotificationType.CourseBulkUpdateEnd,
                        OrganizationId = _workContext.User.Id,
                        ReceiverId = _workContext.User.Id
                    };

                    _notificationHubFactory.Add(notification);
                    result.HasError = true;
                    result.Message = "Inconsistent details where found in your excel. Please validate all fields and try again";
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                /// delete FILE
                File.Delete(fileFullPath);
            }
      
            /// alert our background job to fire this

            return Ok("File uploaded successfully");

        }


    }

}