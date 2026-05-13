using Universe.Core.Contracts.Student;

namespace Universe.Core.Contracts.Enrollments;

public record EnrollmentPageResponse(
    StudentInfoResponse Student,
    List<CourseRegistrationData> Courses,
    List<StudentExistingEnrollment> EnrollmentInfos 
);
