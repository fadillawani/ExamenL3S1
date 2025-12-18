using System;

namespace VueClient.Models;

public partial class PanierItem
{
    public long id { get; set; }

    public long panier_id { get; set; }
    public virtual Panier panier { get; set; } = null!;

    public long? burger_id { get; set; }
    public long? menu_id { get; set; }
    public long? complement_id { get; set; }

    public int quantite { get; set; }
    public double prix_total { get; set; }

    public virtual Burger? burger { get; set; }
    public virtual Menu? menu { get; set; }
    public virtual Complement? complement { get; set; }
}
