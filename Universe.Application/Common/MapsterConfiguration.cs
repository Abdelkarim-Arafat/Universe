using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.Common;

public class MapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CourseOffering, CourseOfferingResponse>()
            .Map(dest => dest.Assessments, src => src.Assessments.Where(a => !a.IsDeleted));

        config.NewConfig<UpdateCourseOfferingCommand , CourseOffering>()
            .Ignore(dest => dest.Assessments);
    }
}
