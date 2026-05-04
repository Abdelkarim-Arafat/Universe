
using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class CourseSeed
{
    public static readonly Course[] Data =
    {
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-95890ee5ba62"), Name = "Programming 1", Code = "CS101", Description = "Introduction to programming concepts.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958a05c9c32a"), Name = "Programming 2", Code = "CS102", Description = "Advanced programming and problem solving.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958b81dee3d6"), Name = "Object Oriented Programming", Code = "CS103", Description = "Concepts of OOP and design principles.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958c542ea45f"), Name = "Data Structures", Code = "CS201", Description = "Data organization and structures.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958d58bae05b"), Name = "Algorithms", Code = "CS202", Description = "Algorithm design and analysis.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958e1b56b349"), Name = "Database Systems", Code = "CS203", Description = "Database design and SQL.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-958f9e72f562"), Name = "Operating Systems", Code = "CS204", Description = "Concepts of OS and processes.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9590bdb364c0"), Name = "Computer Networks", Code = "CS205", Description = "Network architecture and protocols.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9591262e88f8"), Name = "Software Engineering", Code = "CS301", Description = "Software development lifecycle.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-95920fdf4a77"), Name = "Artificial Intelligence", Code = "AI301", Description = "Introduction to AI concepts.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9593115238f8"), Name = "Machine Learning", Code = "AI302", Description = "Supervised and unsupervised learning.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9594f9d4ca6a"), Name = "Deep Learning", Code = "AI303", Description = "Neural networks and deep models.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9595b8b7914a"), Name = "Computer Vision", Code = "AI304", Description = "Image processing and vision systems.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-959678c41dea"), Name = "Natural Language Processing", Code = "AI305", Description = "Text processing and NLP techniques.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-95977f4e315c"), Name = "Cyber Security", Code = "CS302", Description = "Security principles and practices.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9598a067e97e"), Name = "Cryptography", Code = "CS303", Description = "Encryption and security algorithms.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-9599074ce97b"), Name = "Distributed Systems", Code = "CS304", Description = "Concepts of distributed computing.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-959aa16694f7"), Name = "Cloud Computing", Code = "CS305", Description = "Cloud platforms and services.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-959b0bc724f7"), Name = "Human Computer Interaction", Code = "CS306", Description = "User interface and UX design.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) },
        new Course { Id = Guid.Parse("019df349-a51c-7aba-9caf-959c5b7e0ee1"), Name = "Compiler Design", Code = "CS307", Description = "Design of compilers and interpreters.", CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc) }
    };
}