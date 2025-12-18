using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class Panier
{
    public long id { get; set; }

    public long? client_id { get; set; }
    public virtual Users? client { get; set; }

    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }

    public bool is_validated { get; set; }

    public virtual ICollection<PanierItem> panier_item { get; set; }
        = new List<PanierItem>();
}
