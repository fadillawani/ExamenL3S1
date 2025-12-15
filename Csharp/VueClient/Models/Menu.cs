using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class Menu
{
    public long id { get; set; }

    public string libelle { get; set; } = null!;

    public string? image_url { get; set; }

    public bool? is_archived { get; set; }

    public double prix { get; set; }

    public virtual ICollection<CommandeItem> commande_item { get; set; } = new List<CommandeItem>();

    public virtual ICollection<MenuBurger> menu_burger { get; set; } = new List<MenuBurger>();

    public virtual ICollection<MenuComplement> menu_complement { get; set; } = new List<MenuComplement>();
}
