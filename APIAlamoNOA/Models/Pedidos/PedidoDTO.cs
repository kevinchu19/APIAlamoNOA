namespace APIAlamoNoa.Models.Pedidos
{
    public class PedidoDTO
    {
        
        public string identificador {get;set;}
        public int numeroPedido { get; set; }
        public string numeroCliente { get; set; }
        public string? observaciones { get; set; }
        public string fechaDeMovimiento { get; set; }


        //public List<MetaDataDTO> meta_data { get; set; } 
        public ICollection<ItemDTO> items { get; set; }
    }
}
