namespace APIAlamoNOA.Models.CuentaCorriente
{
    public class PedidosPendientesDTO
    {
        public string? Cliente { get; set; }
        public string? Fecha { get; set; }
        public string? TipoComprobante { get; set; }
        public int? NroComprobante { get; set; }
        public string? CodProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal? ImportePendiente { get; set; }
        public decimal? CantidadOriginal { get; set; }
        public decimal? CantidadPendiente { get; set; }
        public string? Estado { get; set; }
    }
}
