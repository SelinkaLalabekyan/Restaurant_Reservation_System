using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;

public class ReservationRepository : IReservationRepository
{
    private readonly RestaurantDbContext _context;

    public ReservationRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Table)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Reservations.FindAsync(id);
        if (item != null)
            _context.Reservations.Remove(item);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}