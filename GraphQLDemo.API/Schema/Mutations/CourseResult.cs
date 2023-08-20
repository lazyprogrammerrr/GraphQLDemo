using static GraphQLDemo.API.Schema.Querys.CourseType;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubjectsEnum Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
