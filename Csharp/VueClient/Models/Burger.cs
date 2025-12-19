using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class Burger
{
    public long id { get; set; }

    public string libelle { get; set; } = null!;

    public string? description { get; set; }

    public double prix { get; set; }

    public string? image_url { get; set; }

    public bool? is_archived { get; set; }

    public long? burger_categorie_id { get; set; }

    public virtual BurgerCategorie? burger_categorie { get; set; }

    public ICollection<PanierItem> panier_item { get; set; } = new List<PanierItem>();

    public virtual ICollection<MenuBurger> menu_burger { get; set; } = new List<MenuBurger>();
}
