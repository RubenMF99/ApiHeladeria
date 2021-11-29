using System;
namespace Restaurante.Models
{
    public class Reservas
    {
        public int Idreserva { get; set; }
        public int Idservicio { get; set; }
        public int Cedula { get; set; }
        public string Email { get; set; }
        public int Numper { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Indicaciones { get; set; }
    }
}
