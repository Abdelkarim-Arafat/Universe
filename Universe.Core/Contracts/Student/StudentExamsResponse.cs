namespace Universe.Core.Contracts.Student;

public record StudentExamsResponse
(
    string StudentName,
    string StudentCode,
    IEnumerable<StudentExam> Exams
);
public record StudentExam
(
    string ExamName,
    IEnumerable<StudentExamPerCourse> Courses
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
