using System;
using System.Collections.Generic;
using VueClient.Models.Enum;

namespace VueClient.Models;


public partial class Commande
{
    public long id { get; set; }

    public long? client_id { get; set; }

    public string? adresse { get; set; }

    public long? quartier_id { get; set; }

    public double montant_hors_livraison { get; set; }

    public double montant_total { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public bool? is_paid { get; set; }

    public virtual Users? client { get; set; }

    public virtual ICollection<CommandeItem> commande_item { get; set; } = new List<CommandeItem>();

    public virtual LivraisonAffectation? livraison_affection { get; set; }
    
    public StatutCommande Statut { get; set; } = StatutCommande.EN_ATTENTE;
    public TypeRetrait TypeRetrait { get; set; }

    public virtual Paiement? paiement { get; set; }
    public virtual Quartier? quartier { get; set; }
}
