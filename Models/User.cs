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

        [Required(ErrorMessage = "Este campos é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Este campos é obrigatório")]
        [DisplayName("CPF")]
        public string Document { get; set; }

        [Required(ErrorMessage = "Este campos é obrigatório")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campos é obrigatório")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campos é obrigatório")]
        [DisplayName("Perfil do Usuário")]
        public UserProfile Profile { get; set; }

    }

    public enum UserProfile
    {
        Admin, User
    }
}
