using RichardLawley.EF.AttributeConfig;
using System.ComponentModel.DataAnnotations;

namespace ExercicioFinal.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required]
        [DecimalPrecision(18, 2)]
        public decimal Preco { get; set; }

    }
}