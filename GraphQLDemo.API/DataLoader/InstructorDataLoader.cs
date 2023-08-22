using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.DataLoader
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options = null, InstructorRepository instructorRepository = null) : base(batchScheduler, options)
        {
            _instructorRepository = instructorRepository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructorDTOs = await _instructorRepository.GetManyIds(keys);

            return instructorDTOs.ToDictionary(i => i.Id);
        }
    }
}
