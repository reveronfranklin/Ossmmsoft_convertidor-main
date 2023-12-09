using System;
namespace Convertidor.Data.Entities.Rh
{
	public class RH_DOCUMENTOS_DETALLES
    {
        public int CODIGO_DOCUMENTO_DETALLE { get; set; }
        public int CODIGO_DOCUMENTO { get; set; }
        public string DESCRIPCION { get; set; } 
        public string EXTRA1 { get; set; } 
        public string EXTRA2 { get; set; }
        public string EXTRA3 { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public DateTime FECHA_FINAL { get; set; }
        public DateTime FECHA_INICIAL { get; set; } 
        
    }
}

