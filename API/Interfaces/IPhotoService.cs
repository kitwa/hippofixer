using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, int id);
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string folderName);
        Task<ImageUploadResult> AddPhotoWithPathAsync(string file, string folderName);

        Task<DeletionResult> DeletePhotoAsync(string publicId);

        Task<DelResResult> DeletePhotosAsync(List<string> publicIds);
        
    }
}