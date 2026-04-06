using RestaurantApi.Models;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task AddAsync(Reservation reservation);
    Task DeleteAsync(int id);
    Task SaveAsync();
}