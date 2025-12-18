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

   
public IActionResult Detail(long id)
{
    var burger = _context.burger
        .FirstOrDefault(b => b.id == id && (b.is_archived == false || b.is_archived == null));

    if (burger == null)
        return NotFound();

    var complements = _context.complement
        .Where(c => c.is_archived == false || c.is_archived == null)
        .ToList();

    var vm = new BurgerDetailsViewModel
    {
        Burger = burger,
        Complements = complements
    };

    return View(vm);
}

}
