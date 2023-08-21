using static GraphQLDemo.API.Models.Subject;

namespace GraphQLDemo.API.Schema.Querys
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubjectsEnum Subject { get; set; }
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }
    }
}
