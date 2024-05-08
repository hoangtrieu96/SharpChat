using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpChat.Models;

public class Password
{
    [Key]
    public long OfUserId { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Hash { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Salt { get; set; }

    public virtual User? User { get; set;}
}
