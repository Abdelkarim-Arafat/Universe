using Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;
using Universe.Core.Entities.StudentInfo;

namespace Universe.Application.Common;

public class MapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CourseOffering , Semester) , CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.Assessments, src => src.Item1.Assessments.Where(a => !a.IsDeleted))
            .Map(dest => dest.SemesterType, src => src.Item2.Name);



        config.NewConfig<AddAssessments , CourseOfferingAssessment>();

        config.NewConfig<UpdateCourseOfferingCommand , CourseOffering>()
            .Ignore(dest => dest.Assessments);

        config.NewConfig<(CourseOffering, AddCourseOfferingCommand), CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.SemesterType, src => src.Item2.SemesterType);

        config.NewConfig<(CourseOffering, UpdateCourseOfferingCommand), CourseOfferingWithDetailsResponse>()
            .Map(dest => dest.SemesterType, src => src.Item2.SemesterType);

        config.NewConfig<Student, PersonalDataResponse>();
        config.NewConfig<ContactInfo, ContactDataResponse>();
        config.NewConfig<ParentInfo, ParentDataResponse>();
        config.NewConfig<MilitaryInfo, MilitaryDataResponse>();
        config.NewConfig<PreviousQualification, PreviousQualificationResponse>();
    }
}
