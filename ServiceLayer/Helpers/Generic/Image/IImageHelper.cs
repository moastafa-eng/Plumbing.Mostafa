using CoreLayer.Enumerators;
using CoreLayer.Models;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Helpers.Generic.Image
{
    public interface IImageHelper
    {
        Task<ImageUploadModel> ImageUpload(string name, IFormFile imageFile, ImageType imageType, string? folderName);
        string DeleteImage(string ImageName);
    }
}
