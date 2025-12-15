using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using VueClient.Models.Enum;

namespace VueClient.Models;

public partial class Users
{
    public long id { get; set; }

    public string nom { get; set; } = null!;

    public string prenom { get; set; } = null!;

    public string tel { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public DateTime? created_at { get; set; }
    public RoleUser? role { get; set; }

    public bool? is_archived { get; set; }

    public virtual ICollection<Commande> commande { get; set; } = new List<Commande>();

    public virtual ICollection<LivraisonAffectation> livraison_affection { get; set; } = new List<LivraisonAffectation>();
}
