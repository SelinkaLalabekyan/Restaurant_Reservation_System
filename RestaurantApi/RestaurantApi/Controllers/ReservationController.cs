using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.ViewModels;

namespace RestaurantApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _service;
    private readonly RestaurantDbContext _context;

    public ReservationController(ReservationService service, RestaurantDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    //  GET: api/reservation

    [HttpPost("create-dto")]
    public async Task<IActionResult> CreateDto([FromBody] ReservationCreateDto dto)
    {
        try
        {
            await _service.Create(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _context.Reservations.FindAsync(id);

        if (r == null)
            return NotFound("Reservation not found");

        if (r.Status != "Cancelled")
            return BadRequest("Reservation is still active ❗");

        _context.Reservations.Remove(r);
        await _context.SaveChangesAsync();

        return Ok("Deleted ✅");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Reservation updated)
    {
        var r = await _context.Reservations.FindAsync(id);

        if (r == null)
            return NotFound();

        
        if (!string.IsNullOrEmpty(updated.Status))
            r.Status = updated.Status;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAllByPage(int page = 1, int pageSize = 10)
    {
        var data = await _context.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Table)
            .OrderByDescending(r => r.ReservationDate) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> Customers()
    {
        return Ok(await _context.Customers.ToListAsync());
    }

    [HttpGet("tables")]
    public async Task<IActionResult> GetTables()
    {
        return Ok(await _context.Tables.ToListAsync());
    }

    [HttpGet("staff")]
    public async Task<IActionResult> GetStaff()
    {
        return Ok(await _context.Staff.ToListAsync());
    }
}