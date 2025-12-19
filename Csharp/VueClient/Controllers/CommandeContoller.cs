using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueClient.Data;
using VueClient.Models;
using VueClient.Models.Enum;
using VueClient.ViewModel;
using System.Security.Claims;

[Authorize]
public class CommandeController : Controller
{
    private readonly AppDbContext _context;

    public CommandeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Valider(ValiderCommandeVM model)
    {
        long clientId = GetCurrentUserId();
        if (clientId == 0)
            return RedirectToAction("Login", "Auth");

        var panier = await _context.panier
            .Include(p => p.panier_item)
            .FirstOrDefaultAsync(p =>
                p.client_id == clientId &&
                p.is_validated == false);

        if (panier == null || !panier.panier_item.Any())
        {
            ModelState.AddModelError("", "Panier vide");
            return RedirectToAction("Index", "Panier");
        }

        double total = panier.panier_item.Sum(i => i.prix_total);

        var commande = new Commande
        {
            PanierId = panier.id,
            UserId = clientId, 
            DateCommande = DateTime.UtcNow,
            PrixTotal = total,
            Etat = StatutCommande.EN_ATTENTE 
        };

        _context.commande.Add(commande);
        await _context.SaveChangesAsync();

        var paiement = new Paiement
        {
            commande_id = commande.Id,
            montant = total,
            MoyenPaiement = model.MoyenPaiement,
            date = DateTime.UtcNow,
            ref_transaction = Guid.NewGuid().ToString()
        };

        _context.paiement.Add(paiement);

        if (model.TypeRetrait == TypeRetrait.LIVRAISON)
        {
            if (model.ZoneId == null)
            {
                ModelState.AddModelError("", "Zone obligatoire pour la livraison");
                return RedirectToAction("Index", "Panier");
            }

            var livraison = new LivraisonAffectation
            {
                commande_id = commande.Id,
                zone_id = model.ZoneId,
                Statut = StatutLivraison.EN_ATTENTE
            };

            _context.livraison_affectation.Add(livraison);
        }

        panier.is_validated = true;

        await _context.SaveChangesAsync();
        var etatValue = commande.Etat;
       Console.WriteLine($"ETAT CLR = {etatValue} ({etatValue.GetType()})");


        return RedirectToAction("Index", "Commande");

    }

    public async Task<IActionResult> Index()
{
    long clientId = GetCurrentUserId();
    if (clientId == 0)
        return RedirectToAction("Login", "Auth");

    var commandes = await _context.commande
        .Where(c => c.UserId == clientId)
        .Include(c => c.Panier)
        .Include(c => c.Paiement)
        .Include(c => c.LivraisonAffectation)
        .ToListAsync();

    return View(commandes);
}


    private long GetCurrentUserId()
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        return claim != null ? long.Parse(claim.Value) : 0;
    }

        public async Task<IActionResult> Detail(long id)
{
    var commande = await _context.commande
        .Include(c => c.Panier)
            .ThenInclude(p => p.client)
        .Include(c => c.Panier)
            .ThenInclude(p => p.panier_item)
                .ThenInclude(i => i.burger)
        .Include(c => c.Panier)
            .ThenInclude(p => p.panier_item)
                .ThenInclude(i => i.menu)
        .Include(c => c.Panier)
            .ThenInclude(p => p.panier_item)
                .ThenInclude(i => i.complement)
        .Include(c => c.Paiement)
        .FirstOrDefaultAsync(c => c.Id == id);

    if (commande == null)
        return NotFound();

    return View(commande);
}

    }
