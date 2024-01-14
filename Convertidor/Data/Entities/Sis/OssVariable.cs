using System;
using System.Collections.Generic;

namespace  Convertidor.Data.Entities.Sis
{
    public partial class OssVariable
    {
        public OssVariable()
        {
            OssCalculos = new HashSet<OssCalculo>();
            OssFormulas = new HashSet<OssFormula>();
        }

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

        public virtual OssModulo? Modulo { get; set; }
        public virtual ICollection<OssCalculo> OssCalculos { get; set; }
        public virtual ICollection<OssFormula> OssFormulas { get; set; }
    }
}
