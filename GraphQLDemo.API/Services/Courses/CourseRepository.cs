using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Courses
{
    public class CourseRepository
    {
        private readonly IDbContextFactory<SchoolDBContext> _dbContextFactory;

        public CourseRepository(IDbContextFactory<SchoolDBContext> contextFactory)
        {
            _dbContextFactory = contextFactory;
        }

        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                return await schoolDBContext.Courses.ToListAsync();
            }
        }

        public async Task<CourseDTO> GetById(Guid courseId)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                return await schoolDBContext.Courses.FirstOrDefaultAsync(c=>c.Id == courseId);
            }
        }

        public async Task<CourseDTO> Create(CourseDTO course)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                schoolDBContext.Courses.Add(course);
                await schoolDBContext.SaveChangesAsync();
                return course;
            }

        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                schoolDBContext.Courses.Update(course);
                await schoolDBContext.SaveChangesAsync();
                return course;
            }

        }

        public async Task<bool> Delete(Guid id)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                CourseDTO courseDTO = new CourseDTO()
                {
                    Id = id
                };
                schoolDBContext.Courses.Remove(courseDTO);
                return await schoolDBContext.SaveChangesAsync() > 0;

            }

        }
    }
}
