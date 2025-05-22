using System;
using System.Collections.Generic;

namespace CompShop.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string? StoreName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<StoreComponent> StoreComponents { get; set; } = new List<StoreComponent>();
}
