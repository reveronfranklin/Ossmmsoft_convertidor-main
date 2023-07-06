using System;
namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_ASIGNACIONESRepository
	{

        Task<bool> PresupuestoExiste(int codPresupuesto);

    }
}

