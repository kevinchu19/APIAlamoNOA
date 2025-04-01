using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Globalization;
using APIAlamoNoa.Models.Pedidos;
using APIAlamoNoa.Repositories;
using APIAlamoNoa.Services;
using APIAlamoNoa.Models.Response.Comprobante;
using APIAlamoNOA.Models.CuentaCorriente;

namespace APIAlamoNoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CuentaCorrienteController : ControllerBase
    {

        private Serilog.ILogger Logger { get; set; }
        public IConfiguration Configuration { get; }
        public CuentaCorrienteRepository Repository { get; }

        public CuentaCorrienteController(Serilog.ILogger logger, IConfiguration configuration, CuentaCorrienteRepository repository)
        {
            Logger = logger;
            Configuration = configuration;
            Repository = repository;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<CuentaCorrienteDTO>>> GetCuentaCorriente(string? numeroCliente, string? numeroVendedor)


        {
            if (numeroCliente == null) { numeroCliente = ""; };
            if (numeroVendedor == null) { numeroVendedor = ""; };
            string ControllerName = "Cuenta Corriente";

 

            Logger.Information($"{ControllerName} - Se recibio consulta de cuenta corriente. Cliente: {numeroCliente}", ControllerName);



            List<CuentaCorrienteDTO?> respuesta = await Repository.ExecuteStoredProcedureList<CuentaCorrienteDTO>
                                                                                     ("Alm_GetCuentaCorrienteForAPI", new Dictionary<string, object>
                                                                                       {
                                                                                            { "@numeroCliente",numeroCliente },
                                                                                            { "@numeroVendedor",numeroVendedor }
                                                                                       }
                                                                                     );

            return Ok(respuesta);

        }
    }
}