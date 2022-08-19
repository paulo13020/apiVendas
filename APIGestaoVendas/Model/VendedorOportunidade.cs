using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGestaoVendas.Model
{
    [Table("OportunidadeVendedor")]
    public class VendedorOportunidade
    {
        [Key]
        public int Id { get; set; }
        public int IdVendedor { get; set; }
        public int idOportunidade { get; set; }
    }
}
