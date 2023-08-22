using GraphQLDemo.API.DataLoader;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructors;
using static GraphQLDemo.API.Models.Subject;

namespace GraphQLDemo.API.Schema.Querys
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubjectsEnum Subject { get; set; }
        [IsProjected(true)]
        public Guid InstructorId { get; set; }
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            InstructorDTO instructorDTO = await instructorDataLoader.LoadAsync(InstructorId);
            return new InstructorType(){
                Id = instructorDTO.Id,  FirstName = instructorDTO.FirstName, LastName = instructorDTO.LastName,Salary = instructorDTO.Salary,
            };
        }
        public IEnumerable<StudentType> Students { get; set; }
    }
}
