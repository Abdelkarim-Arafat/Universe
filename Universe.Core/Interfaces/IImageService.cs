using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces;

public interface IImageService
{
    Task<string> UploadAsync(IFormFile file);
    Task<bool> DeleteAsync(string imageUrl);
    Task<string> UpdateAsync(string oldUrl, IFormFile file);
}