using System;
namespace Restaurante.Models
{
    public class Cliente
    {
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Telefono { get; set; }
        public string Usuario { get; set; }
        public string Password {get; set; }

    }
}