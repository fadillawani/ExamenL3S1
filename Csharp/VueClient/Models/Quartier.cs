using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class Quartier
{
    public long id { get; set; }

    public string nom { get; set; } = null!;

    public long? zone_id { get; set; }


    public virtual Zone? zone { get; set; }
}
