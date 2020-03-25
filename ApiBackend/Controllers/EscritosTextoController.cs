using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public List<EscritosTexto> GetEscritosTextos()
        {
            try
            {
                List<EscritosTexto> listaEscritosTexto = _Context.EscritosTexto.ToList();
                return listaEscritosTexto;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                throw ex;
            }

        }


        [HttpGet("escritoTexto/{escritoTextoID}")]
        public EscritosTexto Get(int escritoTextoID)
        {
            try
            {
                EscritosTexto escritoTexto = _Context.EscritosTexto.Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
                return escritoTexto;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpPost("nuevo")]
        public void Post([FromBody]EscritosTexto escritosTexto)
        {
            try
            {
                _Context.EscritosTexto.Add(escritosTexto);
                _Context.SaveChanges();

                _Logger.LogInformation("Insert Success!!");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}