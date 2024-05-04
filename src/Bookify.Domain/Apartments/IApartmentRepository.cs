namespace Bookify.Domain.Apartments
{
    public interface IApartmentRepository
    {
        Task<Apartment> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
