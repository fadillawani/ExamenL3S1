using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class MenuBurger
{
    public long id { get; set; }

    public long? menu_id { get; set; }

    public long? burger_id { get; set; }

    public int quantite { get; set; }

    public virtual Burger? burger { get; set; }

    public virtual Menu? menu { get; set; }
}
