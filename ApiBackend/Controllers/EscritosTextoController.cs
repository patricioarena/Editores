using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiBackend.Results;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscritosTextoController : ControllerBase
    {
        private readonly POCContext _Context;
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;
        private readonly ILogger<EscritosTextoController> _Logger;
        public EscritosTextoController(POCContext context, ILogger<EscritosTextoController> logger, IConfiguration configuration)
        {
            _Configuration = configuration;
            _Context = context;
            _Logger = logger;
            //_ConnectionString = configuration.GetConnectionString("SQLite");
            _ConnectionString = configuration.GetConnectionString("SQLServer");
            //_ConnectionString = configuration.GetConnectionString("SQLServer2");
        }

        [HttpGet("GetAllEscritosTextos")]
        public IActionResult GetEscritosTextos()
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                List<EscritosTexto> listaEscritosTexto = null;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                //    listaEscritosTexto = _Context.EscritosTexto.ToList();
                //});
                listaEscritosTexto = _Context.EscritosTexto.ToList();
                return Ok(new ResponseApi<List<EscritosTexto>>(HttpStatusCode.OK, "ListaEscritosTexto", listaEscritosTexto));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new CustomController().CustomErrorStatusCode(ex);
            }
        }

        [HttpGet("escritoTexto/{escritoTextoID}")]
        public IActionResult Get(int escritoTextoID)
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                EscritosTexto escritoTexto = null;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                //    escritoTexto = _Context.EscritosTexto.Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
                //});
                escritoTexto = _Context.EscritosTexto.Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
                return Ok(new ResponseApi<EscritosTexto>(HttpStatusCode.OK, "EscritoTexto", escritoTexto));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new CustomController().CustomErrorStatusCode(ex);
            }
        }

        [HttpPost("nuevo")]
        public IActionResult Post([FromBody]EscritosTexto escritosTexto)
        {
            try
            {
                //var callerIdentity = User.Identity as WindowsIdentity;
                //WindowsIdentity.RunImpersonated(callerIdentity.AccessToken, () => {
                _Context.EscritosTexto.Add(escritosTexto);
                _Context.SaveChanges();
                //});
                _Logger.LogInformation("Insert Success!!");
                return Ok(new ResponseApi<EscritosTexto>(HttpStatusCode.OK, "Insert Success!!", null));
            }
            catch (System.Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new CustomController().CustomErrorStatusCode(ex);
            }
        }
    }
}