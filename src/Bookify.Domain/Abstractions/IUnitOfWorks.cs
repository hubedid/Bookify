namespace Bookify.Domain.Abstractions
{
    internal interface IUnitOfWorks
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default); 
    }
}
