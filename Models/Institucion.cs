using System.ComponentModel.DataAnnotations;

namespace Actividad_4.Models
{
    public class Institucion
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Ubicacion { get; set; }

        public ICollection<Investigador> Investigadores { get; set; } = new List<Investigador>();
    }
}