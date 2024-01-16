using System;
using System.Collections.Generic;

namespace Convertidor.Data.Entities.Sis
{
    public partial class OssModeloCalculo
    {
        public OssModeloCalculo()
        {
            OssCalculos = new HashSet<OssCalculo>();
            OssFormulas = new HashSet<OssFormula>();
        }

        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? UsuarioUpd { get; set; }
        public int? CodigoEmpresa { get; set; }

        public virtual ICollection<OssCalculo> OssCalculos { get; set; }
        public virtual ICollection<OssFormula> OssFormulas { get; set; }
    }
}
