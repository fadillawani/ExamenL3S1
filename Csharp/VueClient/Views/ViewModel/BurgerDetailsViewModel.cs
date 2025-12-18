using VueClient.Models;
using System.Collections.Generic;

public class BurgerDetailsViewModel
{
    public Burger Burger { get; set; } = null!;
    public List<Complement> Complements { get; set; } = new();
}
