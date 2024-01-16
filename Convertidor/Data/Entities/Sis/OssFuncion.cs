using System;
using System.Collections.Generic;

namespace Convertidor.Data.Entities.Sis
{
    public partial class OssFuncion
    {
        public int Id { get; set; }
        public string Funcion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }
        public int? ModuloId { get; set; }
        public bool? EsSql { get; set; }

        public virtual OssModulo? Modulo { get; set; }
    }
}
