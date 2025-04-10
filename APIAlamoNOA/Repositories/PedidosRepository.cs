using APIAlamoNOA.Models.Response.Comprobante;
using Microsoft.Data.SqlClient;

namespace APIAlamoNOA.Repositories
{
    public class PedidosRepository : RepositoryBase
    {
        public PedidosRepository(IConfiguration configuration, Serilog.ILogger logger) : base(configuration, logger)
        {
        }

    }
}