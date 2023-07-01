using System.ComponentModel.DataAnnotations;

namespace MsgFoundation.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
