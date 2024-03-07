using System.ComponentModel.DataAnnotations;
namespace learnify.Models;

public class User {
    [Key]
    public Guid UserId { get; set; }
    [Required]
    [MinLength(3, ErrorMessage ="Username must be shorter than 3 letters")]
    [MaxLength(10, ErrorMessage ="Username must not be longer than 10 letters.")]
    public string? Username {get; set;}

    [Required]
    public string? UserType { get; set;}
    [Required]
    [EmailAddress(ErrorMessage ="Invalid Email")]
    public string? Email {get; set;}
    [Required]
    [MinLength(8,ErrorMessage ="Password must be 8 or more characters long.")]
    public string? Password {get; set;}
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set;}

    public List<Resource>? Resources {get; set;}
}