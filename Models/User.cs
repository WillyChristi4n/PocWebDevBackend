using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocWebDevBackend.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Senha")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Perfil")]
        public RoleType Role { get; set; }
    }

    public enum RoleType 
    { 
        Admin,
        User
    }
}
