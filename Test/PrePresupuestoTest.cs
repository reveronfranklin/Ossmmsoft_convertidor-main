﻿using Convertidor.Controllers;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto;
using Moq;

namespace Test;

public class PrePresupuestoTest
{
    PrePresupuestoController _controller;

   
    private readonly Mock<IPRE_PRESUPUESTOSService> _services;

    public PrePresupuestoTest()
    {
      
        _services = new Mock<IPRE_PRESUPUESTOSService>();



    }

  

    private  ResultDto<List<ListPresupuestoDto>> GetPresupuestos()
    {
        ResultDto<List<ListPresupuestoDto>> result = new ResultDto<List<ListPresupuestoDto>>(null!);
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
