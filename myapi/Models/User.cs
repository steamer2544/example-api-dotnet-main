using System.ComponentModel.DataAnnotations;

namespace myapi.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Name { get; set; }
    }
}
