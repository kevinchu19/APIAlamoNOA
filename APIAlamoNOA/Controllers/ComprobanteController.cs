using APIAlamoNOA.Models.Response.Pdf;
using APIAlamoNOA.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIAlamoNOA.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ComprobanteController : ControllerBase
    {

        private readonly ILogger<ComprobanteController> Logger;


        public ComprobanteRepository Repository { get; }

        public ComprobanteController(ILogger<ComprobanteController> logger, ComprobanteRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        [HttpGet]
        [Route("file")]
        public async Task<ActionResult<PdfResponse>> GetFiles(string empresaComprobante, string codigoComprobante, string numeroComprobante, bool pidePdf = true)
        {

            int numeroFormulario = 0;

            bool isNumber = Int32.TryParse(numeroComprobante, out numeroFormulario);

            if (!isNumber)
            {
                return BadRequest(new PdfResponse(new PdfDTO("El parámetro numeroComprobante debe ser numérico")));
            }

            PdfDTO? result = await Repository.GetPdfPath(empresaComprobante, codigoComprobante, numeroFormulario);



            if (result == null)
            {
                return NotFound(new PdfResponse(new PdfDTO("El comprobante solicitado no existe")));
            }

            if (pidePdf)
            {
                if (result.pdf == "")
                {
                    return NotFound(new PdfResponse(new PdfDTO("El comprobante solicitado no tiene pdf generado")));
                }

                try
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(result.pdf);
                    result.pdf = Convert.ToBase64String(bytes);
                    return Ok(new PdfResponse(result));
                }
                catch (Exception ex)
                {

                    return NotFound(new PdfResponse(new PdfDTO("No se encontro el archivo pdf asociado al comprobante solicitado")));
                }
            }

            result.pdf = null;

            return Ok(new PdfResponse(result));
        }

        

    }
}
