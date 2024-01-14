using System;
using System.Collections.Generic;

namespace  Convertidor.Data.Entities.Sis
{
    public partial class OssModulo
    {
        public OssModulo()
        {
            OssCalculos = new HashSet<OssCalculo>();
            OssFormulas = new HashSet<OssFormula>();
            OssVariables = new HashSet<OssVariable>();
        }

        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }

        public virtual ICollection<OssCalculo> OssCalculos { get; set; }
        public virtual ICollection<OssFormula> OssFormulas { get; set; }
        public virtual ICollection<OssVariable> OssVariables { get; set; }
    }
}
