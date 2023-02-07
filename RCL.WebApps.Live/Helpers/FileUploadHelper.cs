using RCL.Core.Azure.BlobStorage;

namespace RCL.WebApps.Live.Helpers
{
    public static class FileUploadHelper
    {
        public static string FILE_OK => "FILE_OK";

        public static string FileChecker(IFormFile formfile, bool isImageFile = false)
        {
            if (formfile == null)
            {
                return "No file was uploaded";
            }
            else
            {
                if (formfile.Length < 1)
                {
                    return "File error";
                }
                else
                {
                    if(formfile.Length > 250 * 1000)
                    {
                        return "Maximum file size of 250 Kb allowed";
                    }
                    if (isImageFile == true)
                    {
                        if (FileHelper.IsImageFile(formfile.FileName) == false)
                        {
                            return "Only jpg, jpeg, png, gif, bmp, svg image files are allowed.";
                        }
                    }
                }
            }

            return FILE_OK;
        }

        public static async Task<string> SaveFileAsync(IFormFile formFile, IAzureBlobStorageService blobStorage)
        {
            string s = string.Empty;

            try
            {
                string fileExtension = FileHelper.GetFileExtension(formFile.FileName);
                string blobName = $"{Guid.NewGuid().ToString()}{fileExtension}";
                using (var readStream = formFile.OpenReadStream())
                {
                    var blob = await blobStorage.UploadBlobAsync(ConstantsHelper.BLOBCONTAINER, ContainerType.Private, blobName, readStream, FileHelper.GetContentType(fileExtension));
                    if (!string.IsNullOrEmpty(blob?.Uri?.ToString() ?? string.Empty))
                    {
                        s = blobName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not upload file {ex.Message}");
            }

            return s;
        }

        public static async Task DeleteFileAsync(string blobName, IAzureBlobStorageService blobStorage)
        {
            try
            {
                await blobStorage.DeleteBlobAsync(ConstantsHelper.BLOBCONTAINER, blobName);
            }
            catch(Exception ex) 
            {
                throw new Exception($"Could not delete file {ex.Message}");
            }
        }
    }
}
