using Actividad_4.Models;

namespace Actividad_4.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Instituciones.Any()) return; // Ya seeded

            var instituciones = new List<Institucion>
            {
                new Institucion { Nombre = "Universidad Nacional", Ubicacion = "Madrid" },
                new Institucion { Nombre = "Instituto de Investigación", Ubicacion = "Barcelona" },
                new Institucion { Nombre = "Centro de Ciencia", Ubicacion = "Valencia" },
                new Institucion { Nombre = "Universidad Politécnica", Ubicacion = "Sevilla" },
                new Institucion { Nombre = "Instituto Tecnológico", Ubicacion = "Bilbao" },
                new Institucion { Nombre = "Centro de Biotecnología", Ubicacion = "Zaragoza" },
                new Institucion { Nombre = "Universidad de Investigación", Ubicacion = "Málaga" },
                new Institucion { Nombre = "Instituto de Física", Ubicacion = "Granada" }
            };
            context.Instituciones.AddRange(instituciones);
            context.SaveChanges();

            var investigadores = new List<Investigador>
            {
                new Investigador { Nombre = "Ana", Apellido = "García", Email = "ana@unal.es", InstitucionId = 1 },
                new Investigador { Nombre = "Carlos", Apellido = "López", Email = "carlos@inst.es", InstitucionId = 2 },
                new Investigador { Nombre = "María", Apellido = "Rodríguez", Email = "maria@centro.es", InstitucionId = 3 },
                new Investigador { Nombre = "Juan", Apellido = "Martínez", Email = "juan@unal.es", InstitucionId = 1 },
                new Investigador { Nombre = "Laura", Apellido = "Sánchez", Email = "laura@inst.es", InstitucionId = 2 },
                new Investigador { Nombre = "Pedro", Apellido = "Fernández", Email = "pedro@up.es", InstitucionId = 4 },
                new Investigador { Nombre = "Sofia", Apellido = "Gómez", Email = "sofia@it.es", InstitucionId = 5 },
                new Investigador { Nombre = "Miguel", Apellido = "Pérez", Email = "miguel@cb.es", InstitucionId = 6 },
                new Investigador { Nombre = "Elena", Apellido = "Díaz", Email = "elena@ui.es", InstitucionId = 7 },
                new Investigador { Nombre = "David", Apellido = "Ruiz", Email = "david@if.es", InstitucionId = 8 },
                new Investigador { Nombre = "Isabel", Apellido = "Hernández", Email = "isabel@unal.es", InstitucionId = 1 },
                new Investigador { Nombre = "Antonio", Apellido = "Jiménez", Email = "antonio@inst.es", InstitucionId = 2 },
                new Investigador { Nombre = "Carmen", Apellido = "Moreno", Email = "carmen@centro.es", InstitucionId = 3 },
                new Investigador { Nombre = "José", Apellido = "Álvarez", Email = "jose@up.es", InstitucionId = 4 },
                new Investigador { Nombre = "Rosa", Apellido = "Romero", Email = "rosa@it.es", InstitucionId = 5 },
                new Investigador { Nombre = "Francisco", Apellido = "Navarro", Email = "francisco@cb.es", InstitucionId = 6 },
                new Investigador { Nombre = "Patricia", Apellido = "Torres", Email = "patricia@ui.es", InstitucionId = 7 },
                new Investigador { Nombre = "Manuel", Apellido = "Ramos", Email = "manuel@if.es", InstitucionId = 8 },
                new Investigador { Nombre = "Lucía", Apellido = "Gil", Email = "lucia@unal.es", InstitucionId = 1 },
                new Investigador { Nombre = "Ángel", Apellido = "Serrano", Email = "angel@inst.es", InstitucionId = 2 }
            };
            context.Investigadores.AddRange(investigadores);
            context.SaveChanges();

            var proyectos = new List<Proyecto>
            {
                new Proyecto { Titulo = "Proyecto IA", Descripcion = "Desarrollo de IA", FechaInicio = DateTime.Parse("2020-01-01"), FechaFin = DateTime.Parse("2023-12-31"), Presupuesto = 100000 },
                new Proyecto { Titulo = "Proyecto Biotecnología", Descripcion = "Avances en biotecnología", FechaInicio = DateTime.Parse("2019-05-01"), Presupuesto = 150000 },
                new Proyecto { Titulo = "Proyecto Energía", Descripcion = "Energías renovables", FechaInicio = DateTime.Parse("2021-03-01"), FechaFin = DateTime.Parse("2024-03-01"), Presupuesto = 200000 },
                new Proyecto { Titulo = "Proyecto Nanotecnología", Descripcion = "Investigación en nanotecnología", FechaInicio = DateTime.Parse("2018-07-01"), FechaFin = DateTime.Parse("2022-06-30"), Presupuesto = 120000 },
                new Proyecto { Titulo = "Proyecto Genómica", Descripcion = "Estudio del genoma humano", FechaInicio = DateTime.Parse("2022-01-15"), Presupuesto = 180000 },
                new Proyecto { Titulo = "Proyecto Robótica", Descripcion = "Desarrollo de robots autónomos", FechaInicio = DateTime.Parse("2020-09-01"), FechaFin = DateTime.Parse("2025-08-31"), Presupuesto = 250000 },
                new Proyecto { Titulo = "Proyecto Cambio Climático", Descripcion = "Impacto del cambio climático", FechaInicio = DateTime.Parse("2017-11-01"), FechaFin = DateTime.Parse("2021-10-31"), Presupuesto = 90000 },
                new Proyecto { Titulo = "Proyecto Ciberseguridad", Descripcion = "Protección de datos digitales", FechaInicio = DateTime.Parse("2023-02-01"), Presupuesto = 130000 },
                new Proyecto { Titulo = "Proyecto Materiales Avanzados", Descripcion = "Nuevos materiales para la industria", FechaInicio = DateTime.Parse("2019-04-01"), FechaFin = DateTime.Parse("2023-03-31"), Presupuesto = 160000 },
                new Proyecto { Titulo = "Proyecto Salud Mental", Descripcion = "Investigación en salud mental", FechaInicio = DateTime.Parse("2021-06-01"), Presupuesto = 110000 }
            };
            context.Proyectos.AddRange(proyectos);
            context.SaveChanges();

            var investigadorProyectos = new List<InvestigadorProyecto>
            {
                new InvestigadorProyecto { InvestigadorId = 1, ProyectoId = 1 },
                new InvestigadorProyecto { InvestigadorId = 2, ProyectoId = 1 },
                new InvestigadorProyecto { InvestigadorId = 3, ProyectoId = 2 },
                new InvestigadorProyecto { InvestigadorId = 4, ProyectoId = 2 },
                new InvestigadorProyecto { InvestigadorId = 5, ProyectoId = 3 },
                new InvestigadorProyecto { InvestigadorId = 1, ProyectoId = 3 },
                new InvestigadorProyecto { InvestigadorId = 6, ProyectoId = 4 },
                new InvestigadorProyecto { InvestigadorId = 7, ProyectoId = 4 },
                new InvestigadorProyecto { InvestigadorId = 8, ProyectoId = 5 },
                new InvestigadorProyecto { InvestigadorId = 9, ProyectoId = 5 },
                new InvestigadorProyecto { InvestigadorId = 10, ProyectoId = 6 },
                new InvestigadorProyecto { InvestigadorId = 11, ProyectoId = 6 },
                new InvestigadorProyecto { InvestigadorId = 12, ProyectoId = 7 },
                new InvestigadorProyecto { InvestigadorId = 13, ProyectoId = 7 },
                new InvestigadorProyecto { InvestigadorId = 14, ProyectoId = 8 },
                new InvestigadorProyecto { InvestigadorId = 15, ProyectoId = 8 },
                new InvestigadorProyecto { InvestigadorId = 16, ProyectoId = 9 },
                new InvestigadorProyecto { InvestigadorId = 17, ProyectoId = 9 },
                new InvestigadorProyecto { InvestigadorId = 18, ProyectoId = 10 },
                new InvestigadorProyecto { InvestigadorId = 19, ProyectoId = 10 },
                new InvestigadorProyecto { InvestigadorId = 20, ProyectoId = 1 },
                new InvestigadorProyecto { InvestigadorId = 2, ProyectoId = 2 },
                new InvestigadorProyecto { InvestigadorId = 3, ProyectoId = 3 },
                new InvestigadorProyecto { InvestigadorId = 4, ProyectoId = 4 },
                new InvestigadorProyecto { InvestigadorId = 5, ProyectoId = 5 }
            };
            context.InvestigadorProyectos.AddRange(investigadorProyectos);
            context.SaveChanges();

            var publicaciones = new List<Publicacion>
            {
                new Publicacion { Titulo = "Avances en IA", Revista = "Journal AI", Año = 2022, ProyectoId = 1 },
                new Publicacion { Titulo = "Biotecnología Moderna", Revista = "BioTech Review", Año = 2021, ProyectoId = 2 },
                new Publicacion { Titulo = "Energía Solar", Revista = "Energy Journal", Año = 2023, ProyectoId = 3 },
                new Publicacion { Titulo = "Machine Learning", Revista = "ML Today", Año = 2022, ProyectoId = 1 },
                new Publicacion { Titulo = "Genética Avanzada", Revista = "Genetics", Año = 2020, ProyectoId = 2 },
                new Publicacion { Titulo = "Nanotecnología Aplicada", Revista = "Nano Science", Año = 2021, ProyectoId = 4 },
                new Publicacion { Titulo = "Genómica Humana", Revista = "Genome Research", Año = 2023, ProyectoId = 5 },
                new Publicacion { Titulo = "Robots Autónomos", Revista = "Robotics Journal", Año = 2022, ProyectoId = 6 },
                new Publicacion { Titulo = "Cambio Climático Global", Revista = "Climate Change", Año = 2020, ProyectoId = 7 },
                new Publicacion { Titulo = "Ciberseguridad en la Era Digital", Revista = "Cyber Security", Año = 2024, ProyectoId = 8 },
                new Publicacion { Titulo = "Materiales Compuestos", Revista = "Materials Science", Año = 2021, ProyectoId = 9 },
                new Publicacion { Titulo = "Salud Mental Comunitaria", Revista = "Mental Health Journal", Año = 2022, ProyectoId = 10 },
                new Publicacion { Titulo = "IA en Medicina", Revista = "Medical AI", Año = 2023, ProyectoId = 1 },
                new Publicacion { Titulo = "Biotecnología Sostenible", Revista = "Sustainable Bio", Año = 2022, ProyectoId = 2 },
                new Publicacion { Titulo = "Energías Renovables", Revista = "Renewable Energy", Año = 2024, ProyectoId = 3 },
                new Publicacion { Titulo = "Nanopartículas", Revista = "Nano Tech", Año = 2020, ProyectoId = 4 },
                new Publicacion { Titulo = "Edición Genética", Revista = "Genetic Engineering", Año = 2023, ProyectoId = 5 },
                new Publicacion { Titulo = "Robótica Colaborativa", Revista = "Collaborative Robotics", Año = 2021, ProyectoId = 6 },
                new Publicacion { Titulo = "Mitigación del Cambio Climático", Revista = "Climate Mitigation", Año = 2019, ProyectoId = 7 },
                new Publicacion { Titulo = "Inteligencia Artificial en Ciberseguridad", Revista = "AI Security", Año = 2024, ProyectoId = 8 },
                new Publicacion { Titulo = "Polímeros Avanzados", Revista = "Polymer Science", Año = 2022, ProyectoId = 9 },
                new Publicacion { Titulo = "Terapias Psicológicas", Revista = "Psychology Therapy", Año = 2023, ProyectoId = 10 },
                new Publicacion { Titulo = "Aprendizaje Profundo", Revista = "Deep Learning Journal", Año = 2022, ProyectoId = 1 },
                new Publicacion { Titulo = "Ingeniería Tisular", Revista = "Tissue Engineering", Año = 2021, ProyectoId = 2 },
                new Publicacion { Titulo = "Almacenamiento de Energía", Revista = "Energy Storage", Año = 2023, ProyectoId = 3 },
                new Publicacion { Titulo = "Nanomedicina", Revista = "Nanomedicine", Año = 2020, ProyectoId = 4 },
                new Publicacion { Titulo = "Secuenciación del ADN", Revista = "DNA Sequencing", Año = 2023, ProyectoId = 5 },
                new Publicacion { Titulo = "Sistemas Embebidos", Revista = "Embedded Systems", Año = 2022, ProyectoId = 6 },
                new Publicacion { Titulo = "Modelos Climáticos", Revista = "Climate Models", Año = 2020, ProyectoId = 7 },
                new Publicacion { Titulo = "Blockchain en Seguridad", Revista = "Blockchain Security", Año = 2024, ProyectoId = 8 }
            };
            context.Publicaciones.AddRange(publicaciones);
            context.SaveChanges();
        }
    }
}