using CoreLayer.Enumerators;
using CoreLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Generic.Image
{
    public class ImageHelper : IImageHelper
    {
        // Defines variables and constants
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string wwwRoot;
        private const string imageFolder = "Images";
        private const string identityFolder = "user";
        private const string aboutFolder = "aboutUs";
        private const string portfolioFolder = "portfolio";
        private const string teamFolder = "team";
        private const string testimonialfolder = "testimonial";
        private readonly int imageSize = 1024 * 1024;

        // IEnvironment: contains Information about App Environment like App Location
        public ImageHelper(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            // ContantRootBath : contain the Location for the App + wwwroot that contains the static files
            wwwRoot = _hostEnvironment.ContentRootPath + "/wwwroot/";
        }


        // Upload image
        public async Task<ImageUploadModel> ImageUpload(IFormFile imageFile, ImageType imageType, string? folderName)
        {
            // if folder name is null specifies the FolderName based on ImageType Enumerator
            if (folderName == null)
            {
                folderName = GetFolderNameByImageType(imageType);
            }

            // if   Directory is not found create one
            if (!Directory.Exists($"{wwwRoot}/{imageFolder}/{folderName}"))
            {
                CreateDirectory(folderName);
            }

            // Get fileExtention like (png, jpg, jpeg).
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
            
            // if fileExtentions not equal 'jpg' or 'jpeg' return ImageUploadModel Error 
            if (fileExtension != ".jpg" && fileExtension != ".jpeg")
            {
                return new ImageUploadModel { Error = "Pleas only upload JPG or JPEG files" };
            }

            // Creating Unique image By add Microsecond at end of it
            var newFileName = folderName + "_" + DateTime.Now.Microsecond.ToString() + fileExtension;

            string path = Path.Combine($"{wwwRoot}/{imageFolder}/{folderName}", newFileName);

            // prepare the file that will be contains the image inside it
            // using : Disposing the FileStream object after finished
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None,
                imageSize, useAsync: false);

            // copy the image from imageFile to the newFileName
            await imageFile.CopyToAsync(stream);

            // Confirmation that the operation was successful.
            await stream.FlushAsync();

            // return ImgaeUPloadModel with file name and file type.
            return new ImageUploadModel { FileName = $"{folderName}/{newFileName}", FileType = imageFile.ContentType };
        }

        // Delete image
        public string DeleteImage(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwRoot}/{imageFolder}/{imageName}");

            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }

            return "Image is deleted";
        }


        #region **Helper methods**
        private string GetFolderNameByImageType(ImageType imageType)
        {
            string folderName;


            switch (imageType)
            {
                case ImageType.identity:
                    folderName = identityFolder;
                    break;

                case ImageType.about:
                    folderName = aboutFolder;
                    break;

                case ImageType.portfolio:
                    folderName = portfolioFolder;
                    break;

                case ImageType.team:
                    folderName = teamFolder;
                    break;

                case ImageType.testimonial:
                    folderName = testimonialfolder;
                    break;
                default:
                    folderName = "Unknown";
                    break;
            }

            return folderName;
        }

        private void CreateDirectory(string? folderName)
        {
            Directory.CreateDirectory($"{wwwRoot}/{imageFolder}/{folderName}");
        }
        #endregion
    }
}
