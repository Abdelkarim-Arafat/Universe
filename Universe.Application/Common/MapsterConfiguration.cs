using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.Common;

public class MapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CourseOffering, CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.Assessments, src => src.Assessments.Where(a => !a.IsDeleted));

        config.NewConfig<UpdateCourseOfferingCommand , CourseOffering>()
            .Ignore(dest => dest.Assessments);

        config.NewConfig<(CourseOffering, AddCourseOfferingCommand), CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.SemesterType, src => src.Item2.SemesterType);

        config.NewConfig<(CourseOffering, UpdateCourseOfferingCommand), CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.SemesterType, src => src.Item2.SemesterType);
    }
}
