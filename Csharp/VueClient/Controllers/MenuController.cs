using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueClient.Data;

public class MenuController : Controller
{
    private readonly AppDbContext _context;

    public MenuController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var menus = await _context.menu
            .Where(m => m.is_archived == false || m.is_archived == null)

            .Include(m => m.menu_burger)
                .ThenInclude(mb => mb.burger)

            .Include(m => m.menu_complement)
                .ThenInclude(mc => mc.complement)

            .ToListAsync();

        return View(menus);
    }
}
