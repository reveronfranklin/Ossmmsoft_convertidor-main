namespace Convertidor.Dtos.Rh
{



    public class RhAriResponseDto
    {
        public int CodigoAri { get; set; }
        public int CodigoPersona { get; set; } 
        public DateTime FechaAri { get; set; }
        public string FechaAriString { get; set; }
        public FechaDto FechaAriObj { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int Ut { get; set; }
        public string EmpresaA { get; set; } = string.Empty;
        public string EmpresaB { get; set; } = string.Empty;
        public string EmpresaC { get; set; } = string.Empty;
        public string EmpresaD { get; set; } = string.Empty;
        public int AaBs { get; set; }
        public int AbBs { get; set; }
        public int AcBs { get; set; }
        public int AdBs { get; set; }
        public int C1Bs { get; set; }
        public int C2Bs { get; set; }
        public int C3Bs { get; set; }
        public int C4Bs { get; set; }
        public int EuT { get; set; }
        public int H1Ut { get; set; }
        public int CargaFamiliar { get; set; }
        public int H3Bs { get; set; }
        public int K1Bs { get; set; }
        public int K2Bs { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int JpOr { get; set; }
        public int KpOr { get; set; }



    }
     
}

