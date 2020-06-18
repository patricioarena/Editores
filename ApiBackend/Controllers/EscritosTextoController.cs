using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiBackend.Results;
using Application;
using Application.IServices;
using Application.Services;
using DataAccess;
using Dominio.DTOs;
using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#if DEBUG || PERSONAL
    [AllowAnonymous]
#endif
    public class EscritosTextoController : CustomController
    {

        private readonly ILogger<EscritosTextoController> _Logger;
        public EscritosTextoController(IServiceEscritosTexto service, ILogger<EscritosTextoController> logger) : base(service)
        {
            _Logger = logger;
        }

        [HttpGet("GetAllEscritosTextos")]
        public IActionResult GetAllEscritosTextos()
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                List<EscritosTexto> listaEscritosTexto = null;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                //    listaEscritosTexto = _Context.EscritosTexto.ToList();
                //});
                //listaEscritosTexto = _Context.CreateReal().EscritosTexto.ToList();
                listaEscritosTexto = _ServiceEscritosTexto.GetAllEscritosTextos();
                return Ok(new ResponseApi<List<EscritosTexto>>(HttpStatusCode.OK, "ListaEscritosTexto", listaEscritosTexto));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return CustomErrorStatusCode(ex);
            }
        }

        [HttpGet("GetEscritosTextoById/{escritoTextoID}")]
        public IActionResult GetEscritosTextoById(int escritoTextoID)
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                EscritosTexto escritoTexto = null;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                //    escritoTexto = _Context.EscritosTexto.Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
                //});
                escritoTexto = _ServiceEscritosTexto.GetEscritosTextoById(escritoTextoID);
                return Ok(new ResponseApi<EscritosTexto>(HttpStatusCode.OK, "EscritoTexto", escritoTexto));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return CustomErrorStatusCode(ex);
            }
        }

        /// <summary>
        /// Se usa store procedure para probar DbManager
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUltimoEscritosTexto")]
        public IActionResult GetUltimoEscritosTexto()
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                EscritosTexto escritoTexto = null;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                //    escritoTexto = _Context.EscritosTexto.Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
                //});
                escritoTexto = _ServiceEscritosTexto.GetUltimoEscritosTexto();
                return Ok(new ResponseApi<EscritosTexto>(HttpStatusCode.OK, "EscritoTexto", escritoTexto));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return CustomErrorStatusCode(ex);
            }
        }
        
        [HttpPost("SetEscritoTexto")]
        public IActionResult SetEscritoTexto([FromBody]EscritosTextoDto escritosTexto)
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                _ServiceEscritosTexto.SetEscritoTexto(escritosTexto);
                //});
                _Logger.LogInformation("Insert Success!!");
                return Ok(new ResponseApi<EscritosTexto>(HttpStatusCode.OK, "Insert Success!!", null));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return CustomErrorStatusCode(ex);
            }
        }
    }
}