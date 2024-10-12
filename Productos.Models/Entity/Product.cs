using System;
using System.Collections.Generic;

namespace Productos.Models.Entity;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public int Stock { get; set; }
}
