using Bogus;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Filters;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using static GraphQLDemo.API.Models.Subject;

namespace GraphQLDemo.API.Schema.Querys
{
    public class Query
    {
        private readonly CourseRepository _courseRepository;

        public Query(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Faker<InstructorType> _instructorFaker;
        public Faker<StudentType> _studentFaker;
        public Faker<CourseType> _courseFaker;
        //public Query()
        //{
        //    _instructorFaker = new Faker<InstructorType>().RuleFor(c => c.Id,f=> Guid.NewGuid()).RuleFor(c=>c.FirstName,f=>f.Name.FirstName()).RuleFor(c=>c.LastName,f=>f.Name.LastName()).RuleFor(c=>c.Salary,f=>f.Random.Double(0,10000));

        //    _studentFaker = new Faker<StudentType>().RuleFor(c => c.Id, f => Guid.NewGuid()).RuleFor(c => c.FirstName, f => f.Name.FirstName()).RuleFor(c => c.LastName, f => f.Name.LastName()).RuleFor(c => c.GPA, f => f.Random.Int(1,10));

        //    _courseFaker = new Faker<CourseType>().RuleFor(c => c.Id, f => Guid.NewGuid()).RuleFor(c => c.Name, f => f.Name.JobTitle()).RuleFor(c => c.Subject, f => f.PickRandom<SubjectsEnum>()).RuleFor(c => c.Instructor, f => _instructorFaker.Generate()).RuleFor(c=>c.Students,f=>_studentFaker.Generate(3));

        //}
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> courseDTOs = await _courseRepository.GetAll();
            return courseDTOs.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }

        [UseDbContext(typeof(SchoolDBContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        [UseFiltering(typeof(CourseFilterType))]
        public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDBContext schoolDBContext)
        {
            return schoolDBContext.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }

        public async Task<CourseType> GetCourseById(Guid id)
        {
            CourseDTO courseDTOs = await _courseRepository.GetById(id);
            return new CourseType()
            {
                Id = courseDTOs.Id,
                Name = courseDTOs.Name,
                Subject = courseDTOs.Subject,
                InstructorId = courseDTOs.InstructorId,
            };
        }

        [GraphQLDeprecated("This query is Depreciated")]
        public string instructions => "Aakash Sharma";
    }
}
