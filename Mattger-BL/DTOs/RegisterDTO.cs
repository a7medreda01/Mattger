using System.ComponentModel.DataAnnotations;

public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string FullName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}