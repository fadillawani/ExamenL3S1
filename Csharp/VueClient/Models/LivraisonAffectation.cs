using System;
using System.Collections.Generic;
using VueClient.Models.Enum;
namespace VueClient.Models;

public partial class LivraisonAffectation
{
    public long id { get; set; }

    public long? commande_id { get; set; }

    public long? livreur_id { get; set; }

    public long? zone_id { get; set; }
    

    public StatutLivraison Statut { get; set; }= StatutLivraison.EN_ATTENTE;

    public virtual Commande? commande { get; set; }

    public virtual Users? livreur { get; set; }

    public virtual Zone? zone { get; set; }
}
