using System;
using System.Collections.Generic;

namespace Bookstore_Backend.DAL.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }
}
