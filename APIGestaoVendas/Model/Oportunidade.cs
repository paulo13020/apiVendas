using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGestaoVendas.Model
{
    [Table("Oportunidade")]
    public class Oportunidade
    {
        [Key]
        public int idOportunidade { get; set; }

        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string NomeOportunidade { get; set; }
        [Required]
        public double ValorMonetario { get; set; }
        public string RazaoSocial { get; set; }
        public string descricaoAtividade { get; set; }
        public int codigoEstado { get; set; }
    }
}
