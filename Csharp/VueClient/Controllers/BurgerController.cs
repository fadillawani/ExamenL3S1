using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueClient.Data;

public class BurgerController : Controller
{
    private readonly AppDbContext _context;

    public BurgerController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var burgers = await _context.burger
            .Where(b => b.is_archived == false || b.is_archived == null)
            .ToListAsync();

        return View(burgers);
    }
}
