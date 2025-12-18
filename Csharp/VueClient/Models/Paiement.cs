using System;
using System.Collections.Generic;
using VueClient.Models.Enum;

namespace VueClient.Models;

public partial class Paiement
{
    public long id { get; set; }

    public double montant { get; set; }

    public string? ref_transaction { get; set; }

    public DateTime? date { get; set; }
    public MoyenPaiement MoyenPaiement { get; set; }

    public long? commande_id { get; set; }

    public virtual Commande? commande { get; set; }
}
