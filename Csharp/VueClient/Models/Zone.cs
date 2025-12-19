using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class Zone
{
    public long id { get; set; }

    public string nom { get; set; } = null!;

    public double prix_livraison { get; set; }

    public virtual ICollection<LivraisonAffectation> livraison_affectation { get; set; } = new List<LivraisonAffectation>();

    public virtual ICollection<Quartier> quartier { get; set; } = new List<Quartier>();
}
