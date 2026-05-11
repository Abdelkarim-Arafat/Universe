namespace Universe.Application.Extensions;

public static class GradeExtensions
{
 
    public static async Task<List<GradeResponse>> GetProgramGradesWithCacheAsync(
        this IUnitOfWork unitOfWork,   
        ICacheService cacheService,  
        Guid programId,
        CancellationToken ct)
    {
        var tag = GradeCacheKeys.Tags(programId)[0];

        return await cacheService.GetOrCreateAsync(
            key: tag,
            factory: () => unitOfWork.GradeRepository.GetProgramGradesAsync(programId, ct),
            cancellationToken: ct,
            tags: [tag]
        );
    }
}
