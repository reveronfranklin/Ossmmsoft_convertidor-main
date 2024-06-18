namespace Convertidor.Dtos.Rh
{
	public class ListHistoricoMovimientoDto
    {
        public int CodigoHistoricoNomina { get; set; }
        public int CodigoPersona { get; set; }
        public int Cedula { get; set; }
        public string Foto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Full_Name { get; set; } = string.Empty;
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
        public string DescripcionFrecuenciaPago { get; set; } = string.Empty;
        //public string CodigoSector { get; set; } = string.Empty;
        //public string CodigoPrograma { get; set; } = string.Empty;
        public int TipoCuentaId { get; set; }
        public string DescripcionTipoCuenta { get; set; } = string.Empty;
        public int BancoId { get; set; }
        public string DescripcionBanco { get; set; } = string.Empty;
        public string NoCuenta { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPeriodo { get; set; }
        public DateTime FechaNomina { get; set; } 
        public DateTime FechaIngreso { get; set; }
        public string FechaNominaMov { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string StatusMov { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string? EstadoCivil { get; set; } = string.Empty;
        public string SearchText { get { return $"{TipoNomina}-{Cedula}-{Nombre}-{UnidadEjecutora}-{Apellido}-{DescripcionNacionalidad}-{DescripcionCargo}-{EstadoCivil}-{FechaNominaMov}-{Codigo}-{Denominacion}"; } }

    }
    public class ListHistoricoMovimientoExcelDto
    {
       
        public int Cedula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;    
        public string Nacionalidad { get; set; } = string.Empty;
        public string DescripcionNacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DescripcionStatus { get; set; } = string.Empty;
        public string DescripcionSexo { get; set; } = string.Empty;
        public decimal Sueldo { get; set; }
        public string DescripcionCargo { get; set; } = string.Empty;
        public string TipoNomina { get; set; } = string.Empty;
        public string DescripcionTipoCuenta { get; set; } = string.Empty;
        public string DescripcionBanco { get; set; } = string.Empty;
        public string NoCuenta { get; set; } = string.Empty;
        public DateTime FechaNomina { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string FechaNominaMov { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string StatusMov { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string? EstadoCivil { get; set; } = string.Empty;
      
    }
}

