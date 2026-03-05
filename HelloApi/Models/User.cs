using System.ComponentModel.DataAnnotations;

namespace HelloApi.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
