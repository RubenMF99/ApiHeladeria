using System;
namespace Restaurante.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int Cedula { get; set; }
        public int Idplato { get; set; }
        public DateTime Fecha { get; set; }
    }
}