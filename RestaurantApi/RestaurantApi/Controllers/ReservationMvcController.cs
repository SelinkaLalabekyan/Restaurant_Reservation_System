using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.ViewModels;

public class ReservationMvcController : Controller
{
    private readonly RestaurantDbContext _context;

    public ReservationMvcController(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _context.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Table)
            .Select(r => new ReservationViewModel
            {
                Id = r.ReservationId,
                CustomerName = r.Customer.Name,
                TableInfo = "Table #" + r.TableId,
                Date = r.ReservationDate.ToString(),
                Time = r.StartTime + " - " + r.EndTime,
                GuestCount = r.GuestCount ?? 0,
                Status = r.Status ?? "Unknown"
            })
            .ToListAsync();

        return View(data);
    }
}