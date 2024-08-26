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

namespace APIAlamoNoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {

        private Serilog.ILogger Logger { get; set; }

        public PedidosController(Serilog.ILogger logger, PedidosRepository repository, IConfiguration configuration)
        {
            Logger = logger;
            Repository = repository;
            Configuration = configuration;
        }

        public PedidosRepository Repository { get; }
        public IConfiguration Configuration { get; }

        [HttpPost]
        public async Task<ActionResult<List<ComprobanteResponse>>> PostFacturacion([FromBody] PedidoDTO payload)
        {
            List<ComprobanteResponse> response = new List<ComprobanteResponse>();
            bool dioError = false;

            var ControllerName = "Pedidos";
            string JsonBody = JsonConvert.SerializeObject(payload);
            Logger.Information("{ControllerName} - Body recibido: {JsonBody}", ControllerName, JsonBody);

     

            FieldMapper mapping = new FieldMapper();
            if (!mapping.LoadMappingFile(AppDomain.CurrentDomain.BaseDirectory + @"\Services\FieldMapFiles\Pedidos.json"))
            {
                response.Add(new ComprobanteResponse(new ComprobanteDTO((string?)payload.identificador
                , "400", "Error de configuracion", "No se encontro el archivo de configuracion del endpoint", null)));

                return BadRequest(response);
            };


            string errorMessage = await Repository.ExecuteSqlInsertToTablaSAR(mapping.fieldMap,
                                                                            payload,
                                                                            payload.identificador,
                                                                            Configuration["Facturacion:JobName"]);
            if (errorMessage != "")
            {
                dioError = true;
                response.Add(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "400", "Bad Request", errorMessage, null)));

            }
            else
            {
                response.Add(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "200", "OK", errorMessage, null)));
            };
            

            JsonBody = JsonConvert.SerializeObject(response);
            Logger.Information("{ControllerName} - Respuesta: {JsonBody}", ControllerName, JsonBody);

            if (dioError)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}