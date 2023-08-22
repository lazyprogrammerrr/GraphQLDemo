using GraphQLDemo.API.Schema.Querys;
using HotChocolate.Data.Sorting;

namespace GraphQLDemo.API.Schema.Sorters
{
    public class CourseSortTypes : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Id);
            descriptor.Ignore(c => c.InstructorId);
            base.Configure(descriptor);
        }
    }
}
