using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Globalization;
using APIAlamoNOA.Models.Pedidos;
using APIAlamoNOA.Repositories;
using APIAlamoNOA.Services;
using APIAlamoNOA.Models.Response.Comprobante;
using APIAlamoNOA.Models.CuentaCorriente;

namespace APIAlamoNOA.Controllers
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



            List<CuentaCorrienteClienteDTO?> cuentaCorriente = await Repository.ExecuteStoredProcedureList<CuentaCorrienteClienteDTO>
                                                                                     ("Alm_GetCuentaCorrienteForAPI", new Dictionary<string, object>
                                                                                       {
                                                                                            { "@numeroCliente",numeroCliente },
                                                                                            { "@numeroVendedor",numeroVendedor }
                                                                                       }
                                                                                     );
            List<CuentaCorrienteDTO> respuesta = new List<CuentaCorrienteDTO> ();

            var cuentasAgrupadas = cuentaCorriente
                .Where(cc => cc != null && !string.IsNullOrEmpty(cc.Cliente))
                .GroupBy(cc => cc!.Cliente);

            foreach (var grupo in cuentasAgrupadas)
            {
                var primerRegistro = grupo.FirstOrDefault();

                if (primerRegistro != null)
                {
                    var cuentaDTO = new CuentaCorrienteDTO
                    {
                        Cliente = primerRegistro.Cliente,
                        RazonSocial = primerRegistro.RazonSocial,
                        CuentaCorriente = grupo.ToList()!
                    };

                    respuesta.Add(cuentaDTO);
                }
            }

            return Ok(respuesta);

        }
    }
}