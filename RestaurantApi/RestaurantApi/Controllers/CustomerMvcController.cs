using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;

public class CustomerMvcController : Controller
{
    private readonly RestaurantDbContext _context;

    public CustomerMvcController(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _context.Customers
            .Include(c => c.Reservations)
            .ToListAsync();

        return View(data);
    }

    // 🔥 EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        return View(customer);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Customer model)
    {
        var c = await _context.Customers.FindAsync(model.CustomerId);
        if (c == null) return NotFound();

        c.Name = model.Name;
        c.Phone = model.Phone;
        c.Email = model.Email;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}