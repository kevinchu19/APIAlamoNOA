namespace APIAlamoNOA.Models.Response.Pdf
{
    public class PdfDTO
    {
        public string? mensaje { get; set; }
        public string? pdf { get; set; }
        


        public PdfDTO()
        {

        }

        public PdfDTO(string? _mensaje)
        {
            mensaje = _mensaje;
        }
    }
}
