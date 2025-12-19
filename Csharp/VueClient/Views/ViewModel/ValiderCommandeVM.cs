using System.Collections.Generic;
using VueClient.Models;
using VueClient.Models.Enum;

namespace VueClient.ViewModel
{
    public class ValiderCommandeVM
    {
        public List<PanierItemVM> Items { get; set; } = new List<PanierItemVM>();
        public double Total => Items.Sum(i => i.PrixTotal);

        // Moyen de paiement sélectionné
        public MoyenPaiement MoyenPaiement { get; set; } = MoyenPaiement.WAVE;

        // Liste des zones disponibles
        public List<Zone> Zones { get; set; } = new List<Zone>();

        // Type de retrait (Sur place ou Livraison)
        public TypeRetrait TypeRetrait { get; set; }

        // Rue / adresse de livraison
        public string Rue { get; set; } = string.Empty;

        // Zone choisie
        public long? ZoneId { get; set; }
    }
}
