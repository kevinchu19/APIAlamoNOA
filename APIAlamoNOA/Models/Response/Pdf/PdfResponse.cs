using APIAlamoNOA.Models.Response;

namespace APIAlamoNOA.Models.Response.Pdf
{
    public class PdfResponse : BaseResponse<PdfDTO>
    {
        public PdfResponse(PdfDTO response) : base(response)
        {

        }
    }
}
