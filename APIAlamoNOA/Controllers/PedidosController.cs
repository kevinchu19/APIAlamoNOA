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
    public class PedidosController : ControllerBase
    {

        private Serilog.ILogger Logger { get; set; }
        public PedidosRepository Repository { get; }
        public IConfiguration Configuration { get; }

        public PedidosController(Serilog.ILogger logger, PedidosRepository repository, IConfiguration configuration)
        {
            Logger = logger;
            Repository = repository;
            Configuration = configuration;
        }

        

        [HttpPost]
        public async Task<ActionResult<ComprobanteResponse>> PostFacturacion([FromBody] PedidoDTO payload)
        {
            ComprobanteResponse response = null;
            bool dioError = false;

            var ControllerName = "Pedidos";
            string JsonBody = JsonConvert.SerializeObject(payload);
            Logger.Information("{ControllerName} - Body recibido: {JsonBody}", ControllerName, JsonBody);

     

            FieldMapper mapping = new FieldMapper();
            if (!mapping.LoadMappingFile(AppDomain.CurrentDomain.BaseDirectory + @"\Services\FieldMapFiles\Pedidos.json"))
            {
                response = new ComprobanteResponse(new ComprobanteDTO((string?)payload.identificador
                , "400", "Error de configuracion", "No se encontro el archivo de configuracion del endpoint", null));

                return BadRequest(response);
            };


            string errorMessage = await Repository.ExecuteSqlInsertToTablaSAR(mapping.fieldMap,
                                                                            payload,
                                                                            payload.identificador,
                                                                            Configuration["Facturacion:JobName"]);
            if (errorMessage != "")
            {
                dioError = true;
                response = new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "400", "Bad Request", errorMessage, null));

            }
            else
            {
                response = new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "200", "OK", errorMessage, null));
            };
            

            JsonBody = JsonConvert.SerializeObject(response);
            Logger.Information("{ControllerName} - Respuesta: {JsonBody}", ControllerName, JsonBody);

            if (dioError)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [HttpGet]
        [Route("{identificador}")]
        public async Task<ActionResult<ComprobanteResponse>> GetFacturacion(string identificador)
        {
            ComprobanteResponse respuesta = await Repository.GetTransaccion(identificador, "SAR_FCRMVH");

            switch (respuesta.response.status)
            {
                case "404":
                    return NotFound(respuesta);
                   
                default:
                    return Ok(respuesta);
                   
            }

        }

        [HttpGet]
        [Route("pendientes")]
        public async Task<ActionResult<List<PedidosPendientesDTO>>> GetPedidosPendientes([FromQuery] string? numeroCliente, [FromQuery] string? numeroVendedor)

      //  public async Task<ActionResult<List<PedidosPendientesDTO>>> GetPedidosPendientes(string? numeroCliente, string? numeroVendedor)


        {
            if (numeroCliente == null) { numeroCliente = ""; };
            if (numeroVendedor == null) { numeroVendedor = ""; };
            string ControllerName = "Pedidos Pendientes";



            Logger.Information($"{ControllerName} - Se recibio consulta de pedidos pendientes. Cliente: {numeroCliente}", ControllerName);



            List<PedidosPendientesDTO?> respuesta = await Repository.ExecuteStoredProcedureList<PedidosPendientesDTO>
                                                                                     ("Alm_GetPedidosPendientesForAPI", new Dictionary<string, object>
                                                                                       {
                                                                                            { "@numeroCliente",numeroCliente },
                                                                                            { "@numeroVendedor",numeroVendedor }
                                                                                       }
                                                                                     );

            return Ok(respuesta);

        }

    }
}