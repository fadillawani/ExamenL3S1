
namespace VueClient.ViewModel
{

public class PanierItemVM
    {
        public long PanierItemId { get; set; }     // correspond à l'id de l'élément du panier
        public int Quantite { get; set; }
        public double PrixTotal { get; set; }
        public double PrixUnitaire { get; set; } // à utiliser dans la vue
        public string Libelle { get; set; } = string.Empty; // nom du produit
        public string Type { get; set; } = string.Empty;    // Burger, Menu, Complément

        public Models.Burger? Burger { get; set; }
        public Models.Menu? Menu { get; set; }
        public Models.Complement? Complement { get; set; }
    }
}
