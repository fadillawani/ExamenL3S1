using Microsoft.EntityFrameworkCore;
using VueClient.Data;
using VueClient.Models;
using VueClient.Services;
using VueClient.ViewModel;


namespace VueClient.Services.Impl
{
    
public class PanierServicesImpl : IPanierServices
{
    private readonly AppDbContext _context;

    public PanierServicesImpl(AppDbContext context)
    {
        _context = context;
    }

    public PanierVM GetPanier(long clientId)
    {
        var panier = _context.panier
            .Include(p => p.panier_item)
                .ThenInclude(i => i.burger)
            .Include(p => p.panier_item)
                .ThenInclude(i => i.menu)
            .Include(p => p.panier_item)
                .ThenInclude(i => i.complement)
            .FirstOrDefault(p => p.client_id == clientId && !p.is_validated);

        if (panier == null) return new PanierVM();

        return new PanierVM
        {
            PanierId = panier.id,
            Items = panier.panier_item.Select(i => new PanierItemVM
            {
                PanierItemId = i.id,
                Burger = i.burger,
                Menu = i.menu,
                Complement = i.complement,
                Quantite = i.quantite,
                PrixTotal = i.prix_total
            }).ToList()
        };
    }
        // =============================
        // AJOUTER BURGER
        // =============================
        public void AddBurger(long clientId, long burgerId, int quantite)
        {
            var panier = GetOrCreatePanier(clientId);
            var burger = _context.burger.Find(burgerId);
            if (burger == null) throw new Exception("Burger introuvable");

            var item = panier.panier_item.FirstOrDefault(i => i.burger_id == burgerId);
            if (item != null)
            {
                item.quantite += quantite;
                item.prix_total = item.quantite * burger.prix;
            }
            else
            {
                _context.panier_item.Add(new PanierItem
                {
                    panier_id = panier.id,
                    burger_id = burgerId,
                    quantite = quantite,
                    prix_total = quantite * burger.prix
                });
            }

            panier.updated_at = DateTime.Now;
            _context.SaveChanges();
        }

        // =============================
        // AJOUTER MENU
        // =============================
        public void AddMenu(long clientId, long menuId, int quantite)
        {
            var panier = GetOrCreatePanier(clientId);
            var menu = _context.menu.Find(menuId);
            if (menu == null) throw new Exception("Menu introuvable");

            var item = panier.panier_item.FirstOrDefault(i => i.menu_id == menuId);
            if (item != null)
            {
                item.quantite += quantite;
                item.prix_total = item.quantite * menu.prix;
            }
            else
            {
                _context.panier_item.Add(new PanierItem
                {
                    panier_id = panier.id,
                    menu_id = menuId,
                    quantite = quantite,
                    prix_total = quantite * menu.prix
                });
            }

            panier.updated_at = DateTime.Now;
            _context.SaveChanges();
        }

        // =============================
        // AJOUTER COMPLEMENT
        // =============================
        public void AddComplement(long clientId, long complementId, int quantite)
        {
            var panier = GetOrCreatePanier(clientId);
            var complement = _context.complement.Find(complementId);
            if (complement == null) throw new Exception("Complément introuvable");

            var item = panier.panier_item.FirstOrDefault(i => i.complement_id == complementId);
            if (item != null)
            {
                item.quantite += quantite;
                item.prix_total = item.quantite * complement.prix;
            }
            else
            {
                _context.panier_item.Add(new PanierItem
                {
                    panier_id = panier.id,
                    complement_id = complementId,
                    quantite = quantite,
                    prix_total = quantite * complement.prix
                });
            }

            panier.updated_at = DateTime.Now;
            _context.SaveChanges();
        }

        // =============================
        // SUPPRIMER UN ITEM
        // =============================
        public void RemoveItem(long panierItemId)
        {
            var item = _context.panier_item.Find(panierItemId);
            if (item == null) return;

            _context.panier_item.Remove(item);
            _context.SaveChanges();
        }

        // =============================
        // VIDER LE PANIER
        // =============================
        public void Clear(long clientId)
        {
            var panier = GetOrCreatePanier(clientId);
            _context.panier_item.RemoveRange(panier.panier_item);
            panier.updated_at = DateTime.Now;
            _context.SaveChanges();
        }

        // =============================
        // MÉTHODE PRIVÉE - PANIER ACTIF
        // =============================
        private Panier GetOrCreatePanier(long clientId)
        {
            var panier = _context.panier
                .Include(p => p.panier_item)
                .FirstOrDefault(p => p.client_id == clientId && !p.is_validated);

            if (panier == null)
            {
                panier = new Panier
                {
                    client_id = clientId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                    is_validated = false
                };
                _context.panier.Add(panier);
                _context.SaveChanges();
            }

            return panier;
        }
    }
}
