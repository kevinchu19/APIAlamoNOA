using APIAlamoNOA.Models.Response.Comprobante;
using Microsoft.Data.SqlClient;

namespace APIAlamoNOA.Repositories
{
    public class CuentaCorrienteRepository : RepositoryBase
    {
        public CuentaCorrienteRepository(IConfiguration configuration, Serilog.ILogger logger) : base(configuration, logger)
        {
        }

    }
}