using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class BurgerCategorie
{
    public long id { get; set; }

    public string nom { get; set; } = null!;

    public virtual ICollection<Burger> burger { get; set; } = new List<Burger>();
}
