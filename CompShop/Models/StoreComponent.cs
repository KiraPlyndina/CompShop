using System;
using System.Collections.Generic;

namespace CompShop.Models;

public partial class StoreComponent
{
    public int StoreId { get; set; }

    public string Model { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual Component ModelNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
