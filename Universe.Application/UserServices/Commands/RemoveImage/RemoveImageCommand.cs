using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.RemoveImage;

public record RemoveImageCommand(
    string ImageUrl
) : IRequest<Result>;