using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UploadImage;

public record UploadImageCommand(
    [Required] IFormFile File
) : IRequest<Result<string>>;