namespace Actividad_4.Models
{
    public class HomeViewModel
    {
        public int TotalInstituciones { get; set; }
        public int TotalInvestigadores { get; set; }
        public int TotalProyectos { get; set; }
        public int TotalPublicaciones { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public int PublicacionesUltimoAño { get; set; }
        public decimal PromedioPresupuesto { get; set; }
        public int ProyectosActivos { get; set; }
    }
}