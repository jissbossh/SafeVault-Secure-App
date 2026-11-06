using System.ComponentModel.DataAnnotations;

namespace SafeVault.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Solo se permiten letras, números y guiones bajos.")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";
    }
}
