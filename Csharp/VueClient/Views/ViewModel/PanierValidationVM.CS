namespace VueClient.ViewModel
{
    public class PanierValidationVM
    {
        // Pour l’affichage du panier
        public PanierVM Panier { get; set; } = new();

        // Pour la validation de la commande
        public ValiderCommandeVM Commande { get; set; } = new();
    }
}
