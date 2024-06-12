using System;
using System.Collections.Generic;

namespace Bookstore_Backend.DAL.Entities;

public partial class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Price { get; set; }

    public int ReleaseDate { get; set; }

    public int Quantity { get; set; }
}
