namespace APIAlamoNoa.Models.Pedidos
{
    public class ItemDTO
    {
        public string codigoProducto{ get; set; }
        public decimal cantidad{ get; set; }
        public decimal? bonificacion2{ get; set; }
        public decimal? bonificacion3{ get; set; }

    }
}