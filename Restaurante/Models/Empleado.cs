using System;
namespace Restaurante.Models
{
    public class Empleado
    {
        public int Idempleado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdRestaurante { get;set;}
        public string Imagen{ get;set;}
    }
}
