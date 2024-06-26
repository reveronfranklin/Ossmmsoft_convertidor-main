﻿using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Sis
{
    public partial class OssCalculo
    {
        [Key]
        public int Id { get; set; }
        public int? IdCalculo { get; set; }
        public int? IdVariable { get; set; }
        public string? CodeVariable { get; set; }
        public string? Formula { get; set; }
        public string? FormulaValor { get; set; }
        public string? Valor { get; set; }
        public string? Query { get; set; }
        public int? OrdenCalculo { get; set; }
        public string? CodeVariableExterno { get; set; }
        public string? IdCalculoExterno { get; set; }
        public DateTime? FechaCalculo { get; set; }
        public int? UsuarioIns { get; set; }
        public DateTime? FechaIns { get; set; }
        public int? UsuarioUpd { get; set; }
        public DateTime? FechaUpd { get; set; }
        public int? CodigoEmpresa { get; set; }
        public int? ModuloId { get; set; }
        public int? IdModeloCalculo { get; set; }
        public int AcumulaAlTotal { get; set; }
    }
}
