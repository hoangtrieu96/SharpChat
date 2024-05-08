using System.ComponentModel.DataAnnotations;

namespace SharpChat.Models;

public class User
{
    [Key]
    public long Id { get; set;}

    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public virtual Password? Password { get; set; }
}
