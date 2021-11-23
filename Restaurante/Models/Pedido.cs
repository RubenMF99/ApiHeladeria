using System;
namespace Restaurante.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int Cedula { get; set; }
        public string IdPlato { get; set; }
        public string Fecha { get; set; }
    }
}