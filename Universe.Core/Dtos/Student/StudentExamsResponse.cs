namespace Universe.Core.Dtos.Student;

public record StudentExamsResponse
(
    string StudentName,
    string StudentCode,
    List<StudentExam> Exams
);
public record StudentExam
(
    string ExamName,
    List<StudentExamPerCourse> Courses
);
public record StudentExamPerCourse
(
    DateOnly Date,
    string CourseName,
    string CourseCode,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Place,
    int SeatNumber,
    int CommitteeNumber
);
