namespace Convertidor.Utility
{
	
        public class Paginacion<T> : List<T>
        {
            public int PaginaInicio { get; private set; }
            public int PaginasTotales { get; private set; }

            public Paginacion(List<T> items, int contador, int paginaInicio, int cantidadregistros)
            {
                PaginaInicio = paginaInicio;
                PaginasTotales = (int)Math.Ceiling(contador / (double)cantidadregistros);

                this.AddRange(items);
            }

            public bool PaginasAnteriores => PaginaInicio > 1;
            public bool PaginasPosteriores => PaginaInicio < PaginasTotales;

            public static Paginacion<T> CrearPaginacion(List<T> fuente, int paginaInicio=1, int cantidadregistros=100)
            {
            try
            {
                if (paginaInicio == 0) paginaInicio = 1;
                if (cantidadregistros == 0) cantidadregistros = 100;
                var contador = fuente.Count();
                var items = fuente.Skip((paginaInicio - 1) * cantidadregistros).Take(cantidadregistros).ToList();
                var result = new Paginacion<T>(items, contador, paginaInicio, cantidadregistros);
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
               
            }
        }
    
}

