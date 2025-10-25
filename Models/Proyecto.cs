using System.ComponentModel.DataAnnotations;

namespace Actividad_4.Models
{
    public class Proyecto
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Presupuesto { get; set; }

        public ICollection<InvestigadorProyecto> InvestigadorProyectos { get; set; } = new List<InvestigadorProyecto>();
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}