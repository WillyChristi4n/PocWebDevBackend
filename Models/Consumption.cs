using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocWebDevBackend.Models
{
    [Table("Consumptions")]
    public class Consumption
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Valor")]
        public string Value { get; set; }

        [Display(Name = "Tipo de Combustível")]
        public FuelType Type { get; set; }

        [Display(Name = "Veículo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int VeichleId { get; set; }

        [ForeignKey("VeichleId")]
        public Veichle Veichle { get; set; }
    }

    public enum FuelType
    {
       Gasolina,
       Etanol,
    }

}
