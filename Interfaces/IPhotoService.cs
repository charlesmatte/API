using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        // IFormFile is a file that is uploaded to the server

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}