﻿using System.ComponentModel.DataAnnotations;
namespace Convertidor.Data.Entities.Sis
{
    public partial class OssModulo
    {
        [Key]
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }
    }
}
