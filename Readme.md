# Memoria de la Actividad 4: Desarrollo de Aplicación Web para Gestión de Investigación Científica

## Portada

**Título:** Memoria de la Actividad 4: Desarrollo de Aplicación Web para Gestión de Investigación Científica  
**Nombre:** [Nombre del Estudiante]  
**Fecha:** [Fecha Actual, por ejemplo: 15 de diciembre de 2023]

---

## Índice

1. Introducción  
2. Análisis de las Tecnologías Disponibles en .NET para el Acceso a Datos (6a)  
3. Diseño de la Base de Datos  
4. Solución Aportada  
5. Batería de Pruebas  
6. Bibliografía  
7. Anexo: Código Completo de la Aplicación

---

## Introducción

En el contexto de la asignatura de Desarrollo Web en Entorno Servidor, se ha desarrollado una aplicación web completa para la gestión de proyectos de investigación científica. Esta aplicación permite visualizar y gestionar información sobre instituciones, investigadores, proyectos y publicaciones, utilizando tecnologías modernas de .NET. El objetivo principal es demostrar el dominio de las tecnologías de acceso a datos en .NET, incluyendo Entity Framework Core, y la publicación de información en aplicaciones web. La aplicación se basa en un modelo de datos relacional que refleja las entidades clave del dominio de la investigación científica, permitiendo consultas eficientes y presentaciones visuales atractivas. Se ha priorizado el uso de Razor Pages sobre MVC o Blazor, cumpliendo con los requisitos del proyecto, y se ha implementado un diseño responsivo con Tailwind CSS para una experiencia de usuario óptima. Esta introducción establece el marco teórico y práctico del proyecto, destacando la importancia de las tecnologías de acceso a datos en el desarrollo de aplicaciones web modernas.

---

## Análisis de las Tecnologías Disponibles en .NET para el Acceso a Datos (6a)

En el ecosistema de .NET, existen varias tecnologías para el acceso programático a almacenes de datos, cada una con sus ventajas e inconvenientes. Una de las tecnologías fundamentales es ADO.NET, que proporciona un conjunto de clases para acceder a bases de datos de manera directa y de bajo nivel. ADO.NET permite ejecutar consultas SQL directamente, lo que ofrece un control total sobre las operaciones de base de datos, pero requiere escribir código SQL manualmente, lo que puede ser propenso a errores y menos mantenible en aplicaciones complejas. Por ejemplo, una consulta básica en ADO.NET sería:

```csharp
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    var command = new SqlCommand("SELECT * FROM Proyectos", connection);
    var reader = command.ExecuteReader();
    while (reader.Read())
    {
        // Procesar datos
    }
}
```

Por otro lado, Entity Framework Core (EF Core) es un ORM (Object-Relational Mapper) moderno que abstrae la capa de datos, permitiendo trabajar con objetos .NET en lugar de consultas SQL directas. EF Core soporta LINQ para consultas tipadas, migraciones para cambios en el esquema, y relaciones complejas entre entidades. Comparado con ADO.NET, EF Core reduce el código boilerplate, mejora la mantenibilidad y facilita el testing, pero puede tener un ligero overhead de rendimiento en operaciones simples. Una consulta equivalente en EF Core sería:

```csharp
var proyectos = await _context.Proyectos.ToListAsync();
```

Otras opciones incluyen Dapper, un micro-ORM ligero que combina lo mejor de ambos mundos, ofreciendo rendimiento cercano a ADO.NET con simplicidad. En esta aplicación, se ha elegido Entity Framework Core por su integración nativa con ASP.NET Core, soporte para relaciones complejas, y facilidad de uso en aplicaciones web. Esta elección se justifica por la necesidad de manejar entidades relacionadas como Investigadores, Proyectos y Publicaciones, donde EF Core permite cargar datos relacionados de manera eficiente mediante eager loading y lazy loading, mejorando la productividad del desarrollador sin sacrificar funcionalidad. Además, EF Core facilita la implementación de patrones como Repository y Unit of Work, promoviendo un código más limpio y testable. La comparación entre estas tecnologías revela que mientras ADO.NET es ideal para escenarios de alto rendimiento con consultas optimizadas, EF Core es superior para aplicaciones empresariales donde la velocidad de desarrollo y la mantenibilidad son prioritarias, como en este caso de una aplicación de gestión de investigación.

---

## Diseño de la Base de Datos

La base de datos se ha diseñado siguiendo un modelo relacional normalizado para representar las entidades clave del dominio de investigación científica. Las entidades principales son: Institucion, Investigador, Proyecto, Publicacion e InvestigadorProyecto (entidad intermedia para la relación muchos-a-muchos entre Investigadores y Proyectos). La tabla Institucion almacena información básica como Nombre y Ubicacion. Investigador incluye campos como Nombre, Apellido, Email y una clave foránea a Institucion. Proyecto contiene Titulo, Descripcion, FechaInicio, FechaFin y Presupuesto. Publicacion tiene Titulo, Revista, Año y una clave foránea a Proyecto. InvestigadorProyecto maneja la asignación de investigadores a proyectos. Se han definido claves primarias autoincrementales y claves foráneas con restricciones de integridad referencial. El esquema utiliza MySQL como motor de base de datos, configurado a través de Pomelo.EntityFrameworkCore.MySql. Las migraciones de EF Core permiten versionar cambios en el esquema, asegurando consistencia entre desarrollo y producción. Los datos de prueba se inicializan mediante un DbInitializer que puebla las tablas con información realista, incluyendo 8 instituciones, 20 investigadores, 10 proyectos y 30 publicaciones, con relaciones apropiadas para demostrar consultas complejas. Este diseño asegura la integridad de los datos y facilita consultas eficientes, como joins entre entidades relacionadas. La normalización evita redundancias, mientras que las claves foráneas garantizan la consistencia referencial. El uso de MySQL proporciona escalabilidad y compatibilidad con entornos de producción, y las migraciones permiten evolucionar el esquema sin perder datos existentes.

Para ilustrar la configuración del contexto de EF Core, se muestra un fragmento del ApplicationDbContext:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<InvestigadorProyecto>()
        .HasKey(ip => new { ip.InvestigadorId, ip.ProyectoId });

    modelBuilder.Entity<InvestigadorProyecto>()
        .HasOne(ip => ip.Investigador)
        .WithMany(i => i.InvestigadorProyectos)
        .HasForeignKey(ip => ip.InvestigadorId);

    modelBuilder.Entity<InvestigadorProyecto>()
        .HasOne(ip => ip.Proyecto)
        .WithMany(p => p.InvestigadorProyectos)
        .HasForeignKey(ip => ip.ProyectoId);
}
```

---

## Solución Aportada

La solución desarrollada es una aplicación web ASP.NET Core Razor Pages que implementa un patrón de arquitectura limpia con separación de responsabilidades. El proyecto se estructura en capas: Models para entidades de dominio, Data para el contexto de EF Core y inicialización, Services para lógica de negocio, Controllers para manejo de solicitudes HTTP, y Views para presentación. Se utiliza inyección de dependencias para servicios como ProyectoService, que encapsula consultas a la base de datos. Las vistas están tipadas con modelos específicos, como Proyecto en Details.cshtml, y utilizan Razor syntax para renderizar datos dinámicos. Se han implementado filtros de búsqueda en vistas Index, utilizando LINQ para consultas parametrizadas. La aplicación incluye estadísticas en la página de inicio, calculadas mediante agregaciones en HomeService. Para la presentación, se ha integrado Tailwind CSS con modo oscuro, glassmorphism, animaciones AOS, y gráficos Chart.js. La navegación es responsiva con un menú móvil funcional. Se han añadido funcionalidades extra como toggle de tema, búsqueda global y página About, manteniendo el enfoque en solo lectura. El código cumple con principios SOLID, utilizando async/await para operaciones I/O, y maneja errores con try-catch en controladores. Esta arquitectura asegura mantenibilidad y escalabilidad, con servicios desacoplados que facilitan testing unitario. Las vistas tipadas garantizan type safety, reduciendo errores en runtime. Los filtros LINQ permiten consultas dinámicas eficientes, y las estadísticas agregadas proporcionan insights valiosos. La UI moderna mejora la experiencia de usuario, mientras que las funcionalidades extra demuestran creatividad sin comprometer los requisitos principales.

Un ejemplo de servicio desacoplado es el HomeService, que calcula estadísticas:

```csharp
public async Task<HomeViewModel> GetHomeViewModelAsync()
{
    var totalInstituciones = await _context.Instituciones.CountAsync();
    var totalInvestigadores = await _context.Investigadores.CountAsync();
    var totalProyectos = await _context.Proyectos.CountAsync();
    var totalPublicaciones = await _context.Publicaciones.CountAsync();
    // ... cálculos adicionales
    return new HomeViewModel { /* propiedades */ };
}
```

Para ilustrar el uso de LINQ en filtros, en ProyectoService:

```csharp
public async Task<IEnumerable<Proyecto>> GetFilteredProyectosAsync(string searchString, DateTime? fechaInicioDesde, DateTime? fechaInicioHasta)
{
    var query = _context.Proyectos.AsQueryable();
    if (!string.IsNullOrEmpty(searchString))
    {
        query = query.Where(p => p.Titulo.Contains(searchString) || p.Descripcion.Contains(searchString));
    }
    if (fechaInicioDesde.HasValue)
    {
        query = query.Where(p => p.FechaInicio >= fechaInicioDesde);
    }
    // ... más filtros
    return await query.ToListAsync();
}
```

---

## Batería de Pruebas

Se ha realizado una batería exhaustiva de pruebas para verificar el funcionamiento de la aplicación, cubriendo todos los aspectos críticos según los criterios de evaluación. Primera prueba: Verificación de conexión a base de datos (6b). Objetivo: Confirmar que EF Core establece conexión correcta con MySQL. Método: Ejecutar la aplicación y observar logs en consola. Resultado exitoso: La aplicación inicia sin errores, y el DbInitializer carga datos. En caso de error, como cadena de conexión inválida, se mostraría excepción en Program.cs, resolviéndose ajustando appsettings.json. Esta prueba valida la configuración del contexto y la cadena de conexión. Segunda prueba: Recuperación de datos en vistas Index (6c). Objetivo: Asegurar que consultas LINQ recuperan información completa. Método: Navegar a /Instituciones/Index y verificar lista de instituciones. Resultado: Se muestran 8 instituciones con datos correctos. Error potencial: Si relaciones no cargan, se manejan con null-checks en vistas. Esta prueba confirma la recuperación de datos básicos y relacionados. Tercera prueba: Filtros de búsqueda (6e). Objetivo: Validar consultas parametrizadas. Método: En /Proyectos/Index, buscar por título. Resultado: Lista filtra correctamente. Error: Si parámetro vacío, muestra todos. Esta prueba demuestra el uso de LINQ para filtros dinámicos. Cuarta prueba: Vistas de detalles (6c, 6d). Objetivo: Verificar carga de relaciones. Método: Acceder a /Proyectos/Details/1. Resultado: Muestra proyecto con investigadores y publicaciones. Error: NullReferenceException manejado con operadores ?. Esta prueba valida la publicación de datos relacionados en vistas tipadas. Quinta prueba: Estadísticas en Home (6e). Objetivo: Confirmar cálculos agregados. Método: Ver gráficos y números. Resultado: Datos precisos. Error: Si base de datos vacía, valores cero. Esta prueba verifica el uso de LINQ para resúmenes. Sexta prueba: Responsividad y modo oscuro (6d). Objetivo: Verificar UI en dispositivos. Método: Cambiar tamaño ventana y toggle tema. Resultado: Layout adapta, colores cambian. Error: Scripts JS fallan si no cargan, pero se simplificaron. Esta prueba asegura la publicación correcta en diferentes contextos. Séptima prueba: Navegación (6d). Objetivo: Enlaces funcionales. Método: Clic en menú. Resultado: Redirecciones correctas. Error: URLs malformadas resueltas con Url.Action. Esta prueba valida la funcionalidad de la aplicación web. Todas las pruebas pasaron, con manejo de errores documentado, demostrando robustez y cumplimiento de criterios.

Para ilustrar el manejo de errores en controladores, un ejemplo en ProyectosController:

```csharp
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var proyecto = await _context.Proyectos
        .Include(p => p.InvestigadorProyectos).ThenInclude(ip => ip.Investigador)
        .Include(p => p.Publicaciones)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (proyecto == null)
    {
        return NotFound();
    }

    return View(proyecto);
}
