using System;
namespace Restaurante.Models
{
    public class Reservas
    {
        public int Idreserva { get; set; }
        public int Idservicio { get; set; }
        public string Fecha { get; set; }
        public string Cedula { get; set; }
    }
}
