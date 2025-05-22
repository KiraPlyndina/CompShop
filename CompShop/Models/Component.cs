using System;
using System.Collections.Generic;

namespace CompShop.Models;

public partial class Component
{
    public string Model { get; set; } = null!;

    public string? ComponentName { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<StoreComponent> StoreComponents { get; set; } = new List<StoreComponent>();
}
