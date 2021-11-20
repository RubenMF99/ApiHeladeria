using System;
namespace Restaurante.Models
{
    public class Plato
    {
        public int Idplato { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int Precio { get; set;}
        public int IdRestaurante { get; set; }
    }
}