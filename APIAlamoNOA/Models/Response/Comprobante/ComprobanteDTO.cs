namespace APIAlamoNoa.Models.Response.Comprobante
{
    public class ComprobanteDTO
    {

        public string identificador { get; set; }
        public string status { get; set; }
        public string titulo { get; set; }
        public string mensaje { get; set; }
        public ComprobanteGenerado? comprobantegenerado { get; set; }

        public ComprobanteDTO(string? _identificador, string _status, string _titulo, string _mensaje, ComprobanteGenerado _comprobantegenerado)
        {
            identificador = _identificador;
            status = _status;
            titulo = _titulo;
            mensaje = _mensaje;
            comprobantegenerado = _comprobantegenerado;
        }
    }
}
