using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Universe.Core.Abstractions.Options;
using Universe.Core.Interfaces;

namespace Universe.Infrastructure.Repositories;

internal class CloudinaryService : IImageService
{
    private readonly CloudinarySettings _cloudinarySettings;
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        _cloudinarySettings = cloudinarySettings.Value;

        var account = new Account (
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }
    public async Task<string> UploadAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = Guid.CreateVersion7().ToString()
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        return result.SecureUrl.ToString();
    }

    public async Task<bool> DeleteAsync(string imageUrl)
    {
        var publicId = ExtractPublicId(imageUrl);
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result.Result == "ok";
    }

    public async Task<string> UpdateAsync(string oldUrl, IFormFile file)
    {
        await DeleteAsync(oldUrl);
        return await UploadAsync(file);
    }
    
    private string ExtractPublicId(string url)
    {
        var uri = new Uri(url);
        var fileName = Path.GetFileName(uri.LocalPath);
        return Path.GetFileNameWithoutExtension(fileName);
    }
}
