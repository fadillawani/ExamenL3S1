using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueClient.Data;
using VueClient.Services;
using VueClient.ViewModel;
using System.Security.Claims;
using VueClient.Models;
using Microsoft.AspNetCore.Authorization;

public class PanierController : Controller
{
    private readonly AppDbContext _context;
    private readonly IPanierServices _panierService;

    public PanierController(AppDbContext context, IPanierServices panierService)
    {
        _context = context;
        _panierService = panierService;
    }

    // GET: /Panier
    public async Task<IActionResult> Index()
    {
        long clientId = GetCurrentUserId();
        if (clientId == 0) return RedirectToAction("Login", "Auth");

        var panier = await _context.panier
            .Include(p => p.panier_item)
                .ThenInclude(i => i.burger)
            .Include(p => p.panier_item)
                .ThenInclude(i => i.menu)
            .Include(p => p.panier_item)
                .ThenInclude(i => i.complement)
            .FirstOrDefaultAsync(p => p.client_id == clientId && p.is_validated == false);

        if (panier == null)
            return View(new PanierVM());

        var vm = new PanierVM
        {
            PanierId = panier.id,
            Items = panier.panier_item.Select(i => new PanierItemVM
            {
                PanierItemId = i.id,
                Quantite = i.quantite,
                PrixTotal = i.prix_total,
                Libelle = i.burger != null ? i.burger.libelle :
                          i.menu != null ? i.menu.libelle :
                          i.complement!.libelle,
                PrixUnitaire = i.burger != null ? i.burger.prix :
                               i.menu != null ? i.menu.prix :
                               i.complement!.prix,
                Type = i.burger != null ? "Burger" :
                       i.menu != null ? "Menu" : "Complément"
            }).ToList()
        };

        return View(vm);
    }

    // POST: /Panier/AjouterAuPanier
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AjouterAuPanier(long burgerId, int quantite, long[] complementIds)
    {
        long clientId = GetCurrentUserId();
        if (clientId == 0) return RedirectToAction("Login", "Auth");

        // Récupérer ou créer le panier non validé
        var panier = await _context.panier
            .Include(p => p.panier_item)
            .FirstOrDefaultAsync(p => p.client_id == clientId && p.is_validated == false);

        if (panier == null)
        {
            panier = new Panier
            {
                client_id = clientId,
                is_validated = false,
                created_at = DateTime.Now,
                panier_item = new List<PanierItem>()
            };
            _context.panier.Add(panier);
            await _context.SaveChangesAsync();
        }

        // Ajouter le burger principal
        var burger = await _context.burger
            .FirstOrDefaultAsync(b => b.id == burgerId && (b.is_archived == null || b.is_archived == false));

        if (burger != null)
        {
            panier.panier_item.Add(new PanierItem
            {
                burger_id = burger.id,
                quantite = quantite,
                prix_total = burger.prix * quantite
            });
        }

        // Ajouter les compléments
        if (complementIds != null && complementIds.Length > 0)
        {
            var complements = await _context.complement
                .Where(c => complementIds.Contains(c.id) && (c.is_archived == null || c.is_archived == false))
                .ToListAsync();

            foreach (var c in complements)
            {
                panier.panier_item.Add(new PanierItem
                {
                    complement_id = c.id,
                    quantite = 1, // chaque complément compte 1
                    prix_total = c.prix
                });
            }
        }

        await _context.SaveChangesAsync();

        TempData["Success"] = "Produit ajouté au panier !";
        return RedirectToAction("Index", "Panier");
    }

    // Récupérer l'id de l'utilisateur connecté
    private long GetCurrentUserId()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (claim != null && long.TryParse(claim.Value, out long userId))
                return userId;
        }
        return 0;
    }

    [HttpPost]
[Authorize]
public async Task<IActionResult> AjouterMenuAuPanier(long menuId, int quantite)
{
    long clientId = GetCurrentUserId();
    if (clientId == 0) return RedirectToAction("Login", "Auth");

    // Récupérer ou créer le panier non validé
    var panier = await _context.panier
        .Include(p => p.panier_item)
        .FirstOrDefaultAsync(p => p.client_id == clientId && p.is_validated == false);

    if (panier == null)
    {
        panier = new Panier
        {
            client_id = clientId,
            is_validated = false,
            created_at = DateTime.Now,
            panier_item = new List<PanierItem>()
        };
        _context.panier.Add(panier);
        await _context.SaveChangesAsync();
    }

    // Ajouter le menu au panier
    var menu = await _context.menu.FirstOrDefaultAsync(m => m.id == menuId);
    if (menu != null)
    {
        panier.panier_item.Add(new PanierItem
        {
            menu_id = menu.id,
            quantite = quantite,
            prix_total = menu.prix * quantite
        });
    }

    await _context.SaveChangesAsync();

    return RedirectToAction("Index", "Panier");
}

}
