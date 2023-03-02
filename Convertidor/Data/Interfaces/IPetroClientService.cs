using System;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces
{
	public interface IPetroClientService
	{


        Task<PetroBsGetDto> Post(StringContent data);
        Task<PetroBsGetDto> GetPetroFiat();
        Task<string> GetData();
        Task GetPetro();
        

    }
}

