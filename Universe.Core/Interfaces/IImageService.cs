using Microsoft.AspNetCore.Http;

namespace Universe.Core.Interfaces;

public interface IImageService
{
    Task<string> UploadAsync(IFormFile file);
    Task<bool> DeleteAsync(string imageUrl);
    Task<string> UpdateAsync(string oldUrl, IFormFile file);
}