using System;
using System.Collections.Generic;
using VueClient.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema; 

namespace VueClient.Models
{
    public class Commande
    {
        [Column("id")]
        public long Id { get; set; }

        // Relation vers le panier
        [Column("panier_id")]
        public long PanierId { get; set; }
        public Panier Panier { get; set; } = null!;
         [Column("date_commande")]
        public DateTime DateCommande { get; set; } = DateTime.UtcNow;
        [Column("prix_total")]
        public double PrixTotal { get; set; } = 0;
        [Column("user_id")]
        public long UserId { get; set; }         
        public Users User { get; set; } = null!;  

        [Column("etat")]
        public StatutCommande Etat { get; set; } = StatutCommande.EN_ATTENTE;
        [Column("paiement_id")]
        public long? PaiementId { get; set; }
        public virtual Paiement? Paiement { get; set; }
        [Column("livraison_affectation_id")] // corrige ici
        public long? LivraisonAffectationId { get; set; }

        public virtual LivraisonAffectation? LivraisonAffectation { get; set; }

    }
}
