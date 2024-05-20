using System;
using System.Collections.Generic;

namespace lab11.Models;

public partial class UserDetail
{
    public int Id { get; set; }
    public string? City { get; set; }
    public byte[]? Photo { get; set; }
    public string? UserId { get; set; }
}
