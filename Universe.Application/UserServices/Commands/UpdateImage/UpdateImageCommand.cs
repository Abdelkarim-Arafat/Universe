using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateImage;

public record UpdateImageCommand(
    string OldImageUrl,
    IFormFile NewImageFile
) : IRequest<Result<string>>;
