using System;
using System.Collections.Generic;

namespace VueClient.Models;

public partial class MenuComplement
{
    public long id { get; set; }

    public long? menu_id { get; set; }

    public long? complement_id { get; set; }

    public int quantite { get; set; }

    public virtual Complement? complement { get; set; }

    public virtual Menu? menu { get; set; }
}
