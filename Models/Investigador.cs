using System.ComponentModel.DataAnnotations;

namespace Actividad_4.Models
{
    public class Investigador
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int InstitucionId { get; set; }
        public Institucion Institucion { get; set; }

        public ICollection<InvestigadorProyecto> InvestigadorProyectos { get; set; } = new List<InvestigadorProyecto>();
    }
}