using GraphQLDemo.API.Schema.Querys;
using HotChocolate.Subscriptions;
using System.Linq;
using static GraphQLDemo.API.Schema.Querys.CourseType;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courseTypes;

        public Mutation()
        {
            _courseTypes = new List<CourseResult>();
        }

        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEvent)
        {
            CourseResult createCourse = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId

            };

            _courseTypes.Add(createCourse);

            await topicEvent.SendAsync(nameof(Subscriptions.Subscription.CourseCreated), createCourse);

            return createCourse;
        }

        public async Task<CourseResult> UpdateCourseById(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEvent)
        {
            CourseResult course = _courseTypes.FirstOrDefault(c => c.Id == id);
            if(course == null)
            {
                throw new GraphQLException(new Error("Course Does Not Exists!", "COURSE_NOT_EXISTS"));
                //Error("Course Does Not Exists!", "COURSE_NOT_EXISTS");
            }

            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            string topicName = $"{course.Id}_{nameof(Subscriptions.Subscription.CourseUpdated)}";
            await topicEvent.SendAsync(topicName, course);

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courseTypes.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}
