using System;
namespace Convertidor.Dtos.Rh
{
	public class ListHistoricoMovimientoDto
	{
        public int CodigoPersona { get; set; }
        public int Cedula { get; set; }
        public string Foto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string DescripcionNacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DescripcionStatus { get; set; } = string.Empty;
        public string DescripcionSexo { get; set; } = string.Empty;
        public int CodigoRelacionCargo { get; set; }
        public int CodigoCargo { get; set; }
        public string CargoCodigo { get; set; } = string.Empty;
        public int CodigoIcp { get; set; }
        public int CodigoIcpUbicacion { get; set; }
        public decimal Sueldo { get; set; }
        public string DescripcionCargo { get; set; } = string.Empty;
        public int CodigoTipoNomina { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public int FrecuenciaPagoId { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public int TipoCuentaId { get; set; }
        public string DescripcionTipoCuenta { get; set; } = string.Empty;
        public int BancoId { get; set; }
        public string DescripcionBanco { get; set; } = string.Empty;
        public string NoCuenta { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int UsuarioIns { get; set; }
        public int CodigoPeriodo { get; set; }
        public DateTime FechaNomina { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaNominaMov { get; set; }
        public string Complemento { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string StatusMov { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
    
    }
}

