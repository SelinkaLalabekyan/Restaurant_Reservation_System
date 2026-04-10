using RestaurantApi.Models.ViewModels;
using RestaurantApi.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.ViewModels;

public class ReservationService
{
    private readonly IReservationRepository _repo;
      
    private readonly RestaurantDbContext _context;

    public ReservationService(IReservationRepository repo, RestaurantDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<List<Reservation>> GetAll()
        => await _repo.GetAllAsync();

    public async Task Create(ReservationCreateDto dto)
    {
        if (dto.GuestCount <= 0)
            throw new Exception("Invalid guest count");

        
        var customer = new Customer
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var date = DateOnly.FromDateTime(dto.Date);
        var start = TimeOnly.FromTimeSpan(dto.StartTime);
        var end = TimeOnly.FromTimeSpan(dto.EndTime);

        var isConflict = await _context.Reservations.AnyAsync(r =>
            r.TableId == dto.TableId &&
            r.ReservationDate == date &&
            start < r.EndTime &&
            end > r.StartTime
        );

        if (isConflict)
        {
            throw new Exception("Table already reserved for this time ❗");
        }
        
        var reservation = new Reservation
        {
            CustomerId = customer.CustomerId,
            TableId = dto.TableId,
            StaffId = dto.StaffId,
            ReservationDate = DateOnly.FromDateTime(dto.Date),
            StartTime = TimeOnly.FromTimeSpan(dto.StartTime),
            EndTime = TimeOnly.FromTimeSpan(dto.EndTime),
            GuestCount = dto.GuestCount,
            Status = "Confirmed"
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }
}