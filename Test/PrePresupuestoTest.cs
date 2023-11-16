using System.Globalization;
using AutoMapper;
using Convertidor.Controllers;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NPOI.SS.Formula.Functions;

namespace Test;

public class PrePresupuestoTest
{
    PrePresupuestoController _controller;

   
    private readonly Mock<IPRE_PRESUPUESTOSService> _services;

    public PrePresupuestoTest()
    {
      
        _services = new Mock<IPRE_PRESUPUESTOSService>();



    }

  

    private async Task<ResultDto<List<ListPresupuestoDto>>> GetPresupuestos()
    {
        ResultDto<List<ListPresupuestoDto>> result = new ResultDto<List<ListPresupuestoDto>>(null);
        List<ListPresupuestoDto> data = new List<ListPresupuestoDto>
        {
            new ListPresupuestoDto
            {
                CodigoPresupuesto=17,
                Descripcion="PRESUPUESTO DEL AÑO 2023.",
                Ano=2023

            },
             new ListPresupuestoDto
            {
               CodigoPresupuesto=16,
                Descripcion="PRESUPUESTO DEL AÑO 2022.",
                Ano=2023

            }
        };

        result.Data = data;
        result.IsValid = true;
        result.Message = "";
       
        return result;
    }


}
