using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Querys;
using GraphQLDemo.API.Services.Courses;
using HotChocolate.Subscriptions;
using System.Linq;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly CourseRepository _courseRepository;

        public Mutation(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEvent)
        {
            
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            courseDTO = await _courseRepository.Create(courseDTO);

            CourseResult createCourse = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId

            };

            await topicEvent.SendAsync(nameof(Subscriptions.Subscription.CourseCreated), createCourse);

            return createCourse;
        }

        public async Task<CourseResult> UpdateCourseById(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEvent)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            courseDTO = await _courseRepository.Update(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId

            }; 

            string topicName = $"{course.Id}_{nameof(Subscriptions.Subscription.CourseUpdated)}";
            await topicEvent.SendAsync(topicName, course);

            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            return await _courseRepository.Delete(id);
        }
    }
}
