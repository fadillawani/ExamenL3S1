using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class CommandeItem
{
    public long id { get; set; }

    public long? commande_id { get; set; }

    public long? burger_id { get; set; }

    public long? menu_id { get; set; }

    public long? complement_id { get; set; }

    public int quantite { get; set; }

    public double prix_total { get; set; }

    public virtual Burger? burger { get; set; }

    public virtual Commande? commande { get; set; }

    public virtual Complement? complement { get; set; }
    public virtual Menu? menu { get; set; }
}
