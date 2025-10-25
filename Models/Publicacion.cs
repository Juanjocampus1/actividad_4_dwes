using System.ComponentModel.DataAnnotations;

namespace Actividad_4.Models
{
    public class Publicacion
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Revista { get; set; }

        [Range(1900, 2100)]
        public int Año { get; set; }

        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
    }
}