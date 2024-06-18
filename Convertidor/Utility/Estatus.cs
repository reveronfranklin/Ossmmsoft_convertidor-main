using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Utility;

public class Estatus
{
    
    public static List<StatusSolicitudModificacion> GetListStatus()
    {
        List<StatusSolicitudModificacion> result = new List<StatusSolicitudModificacion>();
        StatusSolicitudModificacion anulada = new StatusSolicitudModificacion();
        anulada.Codigo = "AN";
        anulada.Descripcion = "ANULADA";
        anulada.Modificable = false;
        StatusSolicitudModificacion pendiente = new StatusSolicitudModificacion();
        pendiente.Codigo = "PE";
        pendiente.Descripcion = "PENDIENTE";
        pendiente.Modificable = true;
        StatusSolicitudModificacion aprobada = new StatusSolicitudModificacion();
        aprobada.Codigo = "AP";
        aprobada.Descripcion = "APROBADA";
        aprobada.Modificable = false;
        result.Add(anulada);
        result.Add(pendiente);
        result.Add(aprobada);
          
        return result;
    }
    
    public static string GetStatus(string status)
    {
        string result = "";
        var tipoNominaObj = GetListStatus().Where(x => x.Codigo == status).First();
        result = tipoNominaObj.Descripcion;
          
        return result;
    }
        
    public static StatusSolicitudModificacion GetStatusObj(string status)
    {
        StatusSolicitudModificacion result ;
        result = GetListStatus().Where(x => x.Codigo == status).First();
          
        return result;
    }
    
}