using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImageUploadService
{
    public interface IImageService
    {
        Task<UploadResult> ImageUploadAsync(IFormFile image);
    }
}