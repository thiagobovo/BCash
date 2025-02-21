using System.ComponentModel.DataAnnotations;

namespace BCash.AuthApi.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
