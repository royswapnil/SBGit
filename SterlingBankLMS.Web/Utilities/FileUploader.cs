using MultipartDataMediaFormatter.Infrastructure;
using SterlingBankLMS.Web.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
namespace SterlingBankLMS.Web.Utilities
{
    /// <summary>
    /// Insert new files and save to appropriate path returning new file name 
    /// </summary>
    public class FileUploader
    {
        public string FileName { get; set; }
        public string TempFolder { get; set; }
        public int MaxFileSizeMB { get; set; }
        public List<String> FileParts { get; set; }

        private readonly List<string> AllowedExts = AppConstants.AllowedFileExtensions;

        private readonly IWorkContext _workContext;

        public FileUploader(IWorkContext workContext)
        {
            FileParts = new List<string>();
            _workContext = workContext;
        }

        public string UploadFile(HttpPostedFile file, string path)
        {

            string pathString = null;
            if (file.InputStream.Length > 0 && path != null && path != "") {
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(file.InputStream)) {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }
                pathString = UploadFile(fileData, file.FileName, path);
            }
            return pathString;
        }

        public string UploadFile(HttpFile file, string path)
        {
            string pathString = null;
            if (file.Buffer.Length > 0 && path != null && path != "") {
                pathString = UploadFile(file.Buffer, file.FileName, path);
            }
            return pathString;
        }

        /// <summary>
        /// Upload file and return new path
        /// </summary>
        /// <param name="file"></param>
        /// <param name="postedFileName"></param>
        /// <param name="pathstring"></param>
        /// <returns></returns>
        public string UploadFile(byte[] file, string postedFileName, string pathstring)
        {

            string path = HostingEnvironment.MapPath(pathstring);

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            var fileName = Path.GetFileName(postedFileName);
            path = RenameFileIfExists(fileName, path, out fileName);

            try {
                System.IO.File.WriteAllBytes(path, file);
                //return fileName;
                return $"{pathstring}{fileName}";//Note(Sam) i touched here too
            }
            catch (Exception ex) {
                return null;
            }
        }

        /// <summary>
        /// Merge Chunked files
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string MergeFile(string FileName)
        {

            string rslt = null;
            // parse out the different tokens from the filename according to the convention
            string partToken = ".part_";
            string baseFileName = FileName.Substring(0, FileName.IndexOf(partToken));
            string trailingTokens = FileName.Substring(FileName.IndexOf(partToken) + partToken.Length);
            int FileIndex = 0;
            int FileCount = 0;
            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
            int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out FileCount);
            // get a list of all file parts in the temp folder
            string Searchpattern = Path.GetFileName(baseFileName) + partToken + "*";
            string[] FilesList = Directory.GetFiles(Path.GetDirectoryName(FileName), Searchpattern);
            //  merge .. improvement would be to confirm individual parts are there / correctly in sequence, a security check would also be important
            // only proceed if we have received all the file chunks
            if (FilesList.Count() == FileCount) {
                try {
                    if (!MergeFileManager.Instance.InUse(baseFileName)) {
                        MergeFileManager.Instance.AddFile(baseFileName);
                        if (File.Exists(baseFileName))
                            File.Delete(baseFileName);
                        // add each file located to a list so we can get them into 
                        // the correct order for rebuilding the file
                        List<SortedFile> MergeList = new List<SortedFile>();
                        foreach (string File in FilesList) {
                            SortedFile sFile = new SortedFile();
                            sFile.FileName = File;
                            baseFileName = File.Substring(0, File.IndexOf(partToken));
                            trailingTokens = File.Substring(File.IndexOf(partToken) + partToken.Length);
                            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
                            sFile.FileOrder = FileIndex;
                            MergeList.Add(sFile);
                        }
                        // sort by the file-part number to ensure we merge back in the correct order
                        var MergeOrder = MergeList.OrderBy(s => s.FileOrder).ToList();
                        using (FileStream FS = new FileStream(baseFileName, FileMode.Create)) {
                            // merge each file chunk back into one contiguous file stream
                            foreach (var chunk in MergeOrder) {
                                try {
                                    using (FileStream fileChunk = new FileStream(chunk.FileName, FileMode.Open)) {
                                        fileChunk.CopyTo(FS);
                                    }

                                }
                                catch (IOException ex) {
                                    throw;
                                }
                            }
                        }

                        // delete split files
                        foreach (var file in FilesList) {
                            var splitFile = new FileInfo(file);
                            splitFile.Delete();
                        }

                        // get merged file and validate
                        var merged = new FileInfo(baseFileName);

                        // delete if disallowed type or copy to main folder 
                        if (!AllowedExts.Any(x => x == Path.GetExtension(merged.Name).Remove(0, 1))) {
                            merged.Delete();
                            return "invalid";
                        }

                        string mimeType = MimeMapping.GetMimeMapping(merged.Name);
                        var FileUploadPath = mimeType.ToLower().Contains("video") ? AppConstants.VideoUploadPath : AppConstants.DocUploadPath;

                        FileUploadPath = FileUploadPath + (int) _workContext.User.OrganizationId + "/";

                        string path = HostingEnvironment.MapPath(FileUploadPath);

                        if (!Directory.Exists(path)) {
                            Directory.CreateDirectory(path);
                        }

                        var fileName = Path.GetFileName(merged.Name);
                        path = RenameFileIfExists(fileName, path, out fileName);
                        merged.MoveTo(path);


                        //rslt = fileName; 
                        rslt = $"{FileUploadPath}{fileName}"; //NOTE(Sam) sorry i had to touch your code.. it would break both our codes.. yours in production, mine in development
                        // unlock the file from singleton
                        MergeFileManager.Instance.RemoveFile(baseFileName);
                    }
                }
                catch (Exception ex) {
                    if (MergeFileManager.Instance.InUse(baseFileName))
                        MergeFileManager.Instance.RemoveFile(baseFileName);
                    throw;
                }
                // use a singleton to stop overlapping processes

            }
            return rslt;
        }


        public string RenameFileIfExists(string postedFileName, string path, out string fileName)
        {

            fileName = Path.GetFileName(postedFileName);
            var ext = Path.GetExtension(postedFileName).Remove(0, 1);
            var filenamewotext = Path.GetFileNameWithoutExtension(postedFileName);

            var pathtocheck = path + "/" + fileName;
            var counter = 1;
            var tempfilename = "";

            if (System.IO.File.Exists(pathtocheck)) {
                while (System.IO.File.Exists(pathtocheck)) {
                    tempfilename = filenamewotext + "(" + counter.ToString() + ")." + ext;
                    pathtocheck = path + "/" + tempfilename;
                    counter += 1;
                }
                fileName = tempfilename;
            }
            path = path + "/" + fileName;
            return path;
        }

    }

    public struct SortedFile
    {
        public int FileOrder { get; set; }
        public String FileName { get; set; }
    }

    public class MergeFileManager
    {
        private static MergeFileManager instance;
        private List<string> MergeFileList;

        private MergeFileManager()
        {
            try {
                MergeFileList = new List<string>();
            }
            catch { }
        }

        public static MergeFileManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MergeFileManager();
                return instance;
            }
        }

        public void AddFile(string BaseFileName)
        {
            MergeFileList.Add(BaseFileName);
        }

        public bool InUse(string BaseFileName)
        {
            return MergeFileList.Contains(BaseFileName);
        }

        public bool RemoveFile(string BaseFileName)
        {
            return MergeFileList.Remove(BaseFileName);
        }
    }

}