﻿using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Rh
{



    public class  ListStatus{
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
    public class PersonasDto
    {
        public int CodigoPersona { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public int PaisNacimientoId { get; set; }
        public int EstadoNacimientoId { get; set; }
        public string NumeroGacetaNacional { get; set; } = string.Empty;
        public int EstadoCivilId { get; set; }
        public decimal Estatura { get; set; }
        public decimal Peso { get; set; }
        public string ManoHabil { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int IdentificacionId { get; set; }
        public int NumeroIdentificacion { get; set; }
       
        public string DercripcionStatus { get { return GetStatus(Status); } }

        private string GetStatus(string status)
        {

            string result = string.Empty;
            List<ListStatus> listStatus = new List<ListStatus>()
            {
                new ListStatus(){Codigo="A",Descripcion="Activo"},
                new ListStatus(){Codigo="E",Descripcion="Egresado"},
                new ListStatus(){Codigo="S",Descripcion="Suspendido"},
            };


            var statusObject = listStatus.Where(s => s.Codigo == status).FirstOrDefault();
            if (statusObject != null)
            {
                result = statusObject.Descripcion;
            }


            return result;
        }






    }

    public class ListSimplePersonaDto
    {
        public int CodigoPersona { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto { get { return $"{Nombre.Trim()} {Apellido.Trim()}"; } }
      
    }

    public class ListPersonasDto
	{
        public int CodigoPersona { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public int PaisNacimientoId { get; set; }
        public int EstadoNacimientoId { get; set; }
        public string NumeroGacetaNacional { get; set; } = string.Empty;
        public int EstadoCivilId { get; set; }
        public decimal Estatura { get; set; }
        public decimal Peso { get; set; }
        public string ManoHabil { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;   
        public string Status { get; set; } = string.Empty;    
        public int IdentificacionId { get; set; }
        public int NumeroIdentificacion { get; set; }
        public List<ListHistoricoMovimientoDto>? HistoricoMovimientoDto { get; set; }
        public List<ListEducacionDto>? EducacionDto { get; set; }
        public List<ListDireccionesDto>? DireccionesDto { get; set; }
        public string DercripcionStatus { get { return GetStatus(Status); } }

        private string GetStatus(string status)
        {

            string result = string.Empty;
            List<ListStatus> listStatus = new List<ListStatus>()
            {
                new ListStatus(){Codigo="A",Descripcion="Activo"},
                new ListStatus(){Codigo="E",Descripcion="Egresado"},
                new ListStatus(){Codigo="S",Descripcion="Suspendido"},
            };
        

            var statusObject= listStatus.Where(s=>s.Codigo==status).FirstOrDefault();
            if (statusObject!= null) {
                result = statusObject.Descripcion;
            }


            return result;
        }






    }
}

