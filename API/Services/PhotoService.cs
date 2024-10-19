using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, int id)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                 using var stream = file.OpenReadStream();
                 var uploadParams = new ImageUploadParams
                 {
                     File = new FileDescription(file.FileName, stream),
                     Transformation = new Transformation().Height(500).Width(500),
                     Folder = Constants.Cloudinary.KibokoPropertyManagerFolder + env + "/" + id
                 };
                 uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string folderName)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                 using var stream = file.OpenReadStream();
                 var uploadParams = new ImageUploadParams
                 {
                     File = new FileDescription(file.FileName, stream),
                    //  Transformation = new Transformation().Height(500).Width(500).Effect("sharpen"),
                     Transformation = new Transformation(),
                     Folder = folderName
                 };
                 uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        
        public async Task<ImageUploadResult> AddPhotoWithPathAsync(string filePath, string folderName)
        {
            var uploadResult = new ImageUploadResult();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (File.Exists(filePath))
            {
                using var stream = File.OpenRead(filePath);
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(Path.GetFileName(filePath), stream),
                    Transformation = new Transformation().Height(500).Width(500),
                    Folder = folderName + "/" + env
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task<DelResResult> DeletePhotosAsync(List<string> publicIds)
        {
            // var deleteParams = new DeletionParams(publicIds);
            DelResResult delResResult = await _cloudinary.DeleteResourcesAsync(publicIds.ToArray());
                if (delResResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Photos deleted successfully.");
            }

            return delResResult;
        }
    }
}