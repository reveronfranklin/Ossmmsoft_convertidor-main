namespace Convertidor.Dtos.Bm
{
	public class RhDocumentosAdjuntosResponseDto
	{

		
		public int CodigoDocumentoAdjunto { get; set; }
		public int CodigoDocumento { get; set; }

		public string Adjunto { get; set; } = string.Empty;
		public string Titulo { get; set; } = string.Empty;
		public string Patch { get; set; } = string.Empty;
      

    }
}

