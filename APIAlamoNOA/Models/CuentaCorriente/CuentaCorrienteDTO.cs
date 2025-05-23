﻿namespace APIAlamoNOA.Models.CuentaCorriente
{
    public class CuentaCorrienteDTO
    {
        public string? Cliente { get; set; }
        public string? RazonSocial { get; set; }
        public List<CuentaCorrienteClienteDTO>? CuentaCorriente { get; set; }
    
    }

    public class CuentaCorrienteClienteDTO
    {
        public string? Cliente { get; set; }
        public string? RazonSocial { get; set; }
        public string? TipoComprobante { get; set; }
        public int? NroComprobante { get; set; }
        public string? FechaEmision { get; set; }
        public string? FechaVencimiento { get; set; }
        public int? Cuota { get; set; }
        public string? Grupo { get; set; }
        public decimal? Importe { get; set; }
        public decimal? Acumulado { get; set; }

    }
}

