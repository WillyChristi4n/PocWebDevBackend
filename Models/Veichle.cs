
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocWebDevBackend.Models
{
    [Table("Veichles")]
    public class Veichle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Ano de Fabricação")]
        public string YearOfManufactor { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Ano do Modelo")]
        public string YearOfModel { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Placa")]
        public string Plate { get; set; }

        public ICollection<Consumption> Consumptions { get; set; }
    }
}
