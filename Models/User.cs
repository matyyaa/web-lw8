using Microsoft.AspNetCore.Identity;

namespace Lab8.Models;

public class User: IdentityUser<Guid>
{
    public string LastName { get; set; }
    public string Variant { get; set; }
    public string Group { get; set; }
    public string Role { get; set; }
}