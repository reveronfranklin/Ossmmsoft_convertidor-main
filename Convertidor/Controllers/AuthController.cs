using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Convertidor.Services;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using IronPdf;
using Convertidor.Dtos;
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SisUsuariosController : ControllerBase
    {

        private readonly ISisUsuarioServices _service;



        public SisUsuariosController(ISisUsuarioServices service)
        {

            _service = service;



        }


        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _service.GetMyName();
            return Ok(userName);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  Login(LoginDto dto)
        {
            var result = await _service.Login(dto);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userName = _service.GetMyName();
            var sisUsuario = await _service.GetByLogin(userName);
            if (!sisUsuario.REFRESHTOKEN.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (sisUsuario.TOKENEXPIRES < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = _service.GetToken(sisUsuario);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var userName = _service.GetMyName();
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                Login= userName
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            var sisUsuario = await _service.GetByLogin(newRefreshToken.Login);

            sisUsuario.REFRESHTOKEN = newRefreshToken.Token;
            sisUsuario.TOKENCREATED = newRefreshToken.Created;
            sisUsuario.TOKENEXPIRES = newRefreshToken.Expires;
            await _service.Update(sisUsuario);
        }





    }
}
