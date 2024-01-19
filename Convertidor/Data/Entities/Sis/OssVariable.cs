
using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Sis
{
    public partial class OssVariable
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? Longitud { get; set; }
        public int? LongitudRedondeo { get; set; }
        public int? LongitudTruncado { get; set; }
        public int? LongitudDecimal { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }
        public int? ModuloId { get; set; }
    }
}
