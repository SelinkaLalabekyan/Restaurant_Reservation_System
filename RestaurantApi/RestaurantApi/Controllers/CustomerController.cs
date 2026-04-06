using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CustomerController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer updated)
    {
        var c = await _context.Customers.FindAsync(id);
        if (c == null) return NotFound();

        c.Name = updated.Name;
        c.Phone = updated.Phone;
        c.Email = updated.Email;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var hasReservations = await _context.Reservations
            .AnyAsync(r => r.CustomerId == id);

        if (hasReservations)
            return BadRequest("Customer has active reservations");

        var c = await _context.Customers.FindAsync(id);
        if (c == null) return NotFound();

        _context.Customers.Remove(c);
        await _context.SaveChangesAsync();

        return Ok();
    }


}