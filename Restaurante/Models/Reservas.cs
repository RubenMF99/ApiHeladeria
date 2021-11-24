using System;
namespace Restaurante.Models
{
    public class Reservas
    {
        public int Idreserva { get; set; }
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public int Cedula { get; set; }
    }
}
