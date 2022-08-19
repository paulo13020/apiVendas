using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGestaoVendas.Model
{
    [Table("Vendedor")]
    public class Vendedor
    {
        [Key]
        public int IdVendedor { get; set; }
        [Required]
        public string NomeVendedor { get; set; }

        [Required]
        [Remote("Vendedor", "Gestao")]
        public string Email { get; set; }

        [Required]
        public Regiao RegiaoResponsavel { get; set; }

    }

    public enum Regiao
    {
        Norte = 1,
        Nordeste = 2,
        Sudeste = 3,
        Sul = 4,
        CentroOeste = 5
    }
}
