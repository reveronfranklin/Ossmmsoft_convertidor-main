using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SisUsuariosController : ControllerBase
    {

        private readonly ISisUsuarioServices _service;

        private IHttpContextAccessor _httpContextAccessor;

        
      

        public SisUsuariosController(ISisUsuarioServices service, IHttpContextAccessor httpContextAccessor)
        {

            _service = service;
            _httpContextAccessor = httpContextAccessor;

           
        }


        [HttpGet, Authorize]
        [Route("[action]")]
        public ActionResult<string> GetMe()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); //_service.GetMyName();
            return Ok(userName);
        }

        [HttpGet, Authorize]
        [Route("[action]")]
        public async Task<ActionResult> GetMenu()
        {
            var userName = string.Empty;
            userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); //_service.GetMyName();
            var menu = await _service.GetMenu(userName);


            return Ok(menu);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  Login(LoginDto dto)
        {

            try
            {
                var result = await _service.Login(dto);
                if (result.AccessToken.Length > 10)
                {
                    var refreshToken = GenerateRefreshToken(result.AccessToken);
                    refreshToken.Login = dto.Login;
                    result.RefreshToken = refreshToken.Refresh_Token;
                    SetRefreshToken(refreshToken);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Usuario o clave invalida");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
           
          
        }

        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<ActionResult<ResultLoginDto>> RefreshToken(ResultRefreshTokenDto refreshTokento)
        {
            ResultLoginDto resultLogin = new ResultLoginDto();
            //3mBx+k508wTfnpi0K+txwKWz64QFdTfsd6cP6G634khKNzs11W6zSaa2ffzx4B7D2LyXppCQcgU9odwFjK2iSQ==
                var refreshToken = refreshTokento.RefreshToken;
            // Request.Cookies["X-Refresh-Token"];
            //var token = Request.Cookies["osmmasoftToken"];
            string? userName = string.Empty;
            userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); 
            var sisUsuario = await _service.GetByLogin(userName);
            if (sisUsuario==null)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            if (!sisUsuario.REFRESHTOKEN.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
                //var esDiferente = "";
            }
            if (sisUsuario.TOKENEXPIRES < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = _service.GetToken(sisUsuario);
            var newRefreshToken = GenerateRefreshToken(token);
            SetRefreshToken(newRefreshToken);
            //ResultRefreshTokenDto result = new ResultRefreshTokenDto();
            //result.accessToken = token;
            //result.refreshToken = newRefreshToken.Refresh_Token;

                resultLogin.Message = "";
            resultLogin.RefreshToken = newRefreshToken.Refresh_Token; 
            resultLogin.AccessToken = token;
            resultLogin.Name = sisUsuario.LOGIN;
            UserData userData = new UserData();
            userData.Id = sisUsuario.CODIGO_USUARIO;
            userData.username = sisUsuario.LOGIN;
            userData.FullName = sisUsuario.USUARIO;
            userData.Role = "admin";
            userData.Email = $"{sisUsuario.LOGIN}@ossmasoft.com";
            resultLogin.UserData = userData;


            return Ok(resultLogin);
        }

        private RefreshToken GenerateRefreshToken(string token)
        {
            string? userName = string.Empty;
            userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            var refreshToken = new RefreshToken
            {
                Refresh_Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Token = token,
                Expires = DateTime.Now.AddDays(30),
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
            Response.Cookies.Append("X-Refresh-Token", newRefreshToken.Refresh_Token, cookieOptions);
            Response.Cookies.Append("X-Auth-Token", newRefreshToken.Token, cookieOptions);
            var sisUsuario = await _service.GetByLogin(newRefreshToken.Login);

            sisUsuario.REFRESHTOKEN = newRefreshToken.Refresh_Token;
            sisUsuario.TOKENCREATED = newRefreshToken.Created;
            sisUsuario.TOKENEXPIRES = newRefreshToken.Expires;
            await _service.Update(sisUsuario);
        }





    }
}
