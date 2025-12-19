using System;
using System.Collections.Generic;
using VueClient.Models.Enum;

namespace VueClient.Models;


public partial class Complement
{
    public long id { get; set; }

    public string libelle { get; set; } = null!;

    public double prix { get; set; }

    public string? image_url { get; set; }

    public bool? is_archived { get; set; }
    

    public string? TypeComplement { get; set; }
    



    public virtual ICollection<MenuComplement> menu_complement { get; set; } = new List<MenuComplement>();
}
