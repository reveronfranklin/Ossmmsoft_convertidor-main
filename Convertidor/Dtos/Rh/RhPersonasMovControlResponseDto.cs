using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Rh
{
    public class RhPersonasMovControlResponseDto
    {
        public int CodigoPersonaMovCtrl { get; set; }
        public int CodigoPersona { get; set; }
        public int CodigoConcepto { get; set; }
        public int ControlAplica { get; set; }
        public string DescripcionControlAplica { get; set; }
        public string DescripcionConcepto { get; set; }
    }
}
   

 


