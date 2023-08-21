using Bogus;
using static GraphQLDemo.API.Models.Subject;

namespace GraphQLDemo.API.Schema.Querys
{
    public class Query
    {
        public Faker<InstructorType> _instructorFaker;
        public Faker<StudentType> _studentFaker;
        public Faker<CourseType> _courseFaker;
        public Query()
        {
            _instructorFaker = new Faker<InstructorType>().RuleFor(c => c.Id,f=> Guid.NewGuid()).RuleFor(c=>c.FirstName,f=>f.Name.FirstName()).RuleFor(c=>c.LastName,f=>f.Name.LastName()).RuleFor(c=>c.Salary,f=>f.Random.Double(0,10000));

            _studentFaker = new Faker<StudentType>().RuleFor(c => c.Id, f => Guid.NewGuid()).RuleFor(c => c.FirstName, f => f.Name.FirstName()).RuleFor(c => c.LastName, f => f.Name.LastName()).RuleFor(c => c.GPA, f => f.Random.Int(1,10));

            _courseFaker = new Faker<CourseType>().RuleFor(c => c.Id, f => Guid.NewGuid()).RuleFor(c => c.Name, f => f.Name.JobTitle()).RuleFor(c => c.Subject, f => f.PickRandom<SubjectsEnum>()).RuleFor(c => c.Instructor, f => _instructorFaker.Generate()).RuleFor(c=>c.Students,f=>_studentFaker.Generate(3));

        }

        public IEnumerable<CourseType> GetCourses()
        {
            return _courseFaker.Generate(5);
        }

        public async Task<CourseType> GetCourseById(Guid id)
        {
            await Task.Delay(5);
            CourseType course = _courseFaker.Generate();
            course.Id = id;
            return course;
        }

        [GraphQLDeprecated("This query is Depreciated")]
        public string instructions => "Aakash Sharma";
    }
}
