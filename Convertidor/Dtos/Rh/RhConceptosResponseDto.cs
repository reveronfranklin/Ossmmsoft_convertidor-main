using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Rh
{
	public class RhConceptosResponseDto
	{
        public int CodigoConcepto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int CodigoTipoNomina { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string TipoConcepto { get; set; } = string.Empty;
        public int ModuloId { get; set; }
        public int CodigoPuc { get; set; }
        public string Status { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int UsuarioIns { get; set; }
        public DateTime FechaIns { get; set; } 
        public int UsuarioUpd { get; set; }
        public DateTime FechaUpd { get; set; }
        public int CodigoEmpresa { get; set; } 
        public int FrecuenciaId { get; set; }
        public int Dedusible { get; set; } 
        public int Automatico { get; set; } 
        
        }

    }

   

 


