using System;
namespace Restaurante.Models
{
    public class Pedido
    {
        public int Idpedido { get; set; }
        public int Cedula { get; set; }
        public string Idplato { get; set; }
        public string Fecha { get; set; }
    }
}