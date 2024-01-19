using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Sis
{
    public partial class OssFormula
    {
        [Key]
        public int Id { get; set; }
        public int IdVariable { get; set; }
        public string CodeVariable { get; set; } = null!;
        public string? Formula { get; set; }
        public string? FormulaDescripcion { get; set; }
        public int? OrdenCalculo { get; set; }
        public int IdModeloCalculo { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }
        public int? ModuloId { get; set; }
        public int AcumulaAlTotal { get; set; }
        
    }
}
