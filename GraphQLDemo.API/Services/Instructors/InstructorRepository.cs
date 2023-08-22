using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GraphQLDemo.API.Services.Instructors
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDBContext> _dbContextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<InstructorDTO> GetById(Guid instructorId)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                return await schoolDBContext.Instructors.FirstOrDefaultAsync(c => c.Id == instructorId);
            }
        }

        public async Task<IEnumerable<InstructorDTO>> GetManyIds(IReadOnlyList<Guid> instructorIds)
        {
            using (SchoolDBContext schoolDBContext = _dbContextFactory.CreateDbContext())
            {
                return await schoolDBContext.Instructors.Where(i=>instructorIds.Contains(i.Id)).ToListAsync();
            }
        }
    }
}
