namespace Actividad_4.Models
{
    public class InvestigadorProyecto
    {
        public int InvestigadorId { get; set; }
        public Investigador Investigador { get; set; }

        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
    }
}