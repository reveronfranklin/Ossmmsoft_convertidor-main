﻿using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Convertidor.Services;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using IronPdf;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmProveedoresController : ControllerBase
    {
       
        private readonly IAdmProveedoresService _service;

        public AdmProveedoresController(IAdmProveedoresService service)
        {

            _service = service;


        }
        

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(AdmProveedorFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmProveedorUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmProveedorUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmProveedorDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
