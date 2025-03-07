using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TecnySystem.Data;
using TecnySystem.SumbaServices.Sumba.Business.Implemetacion;
using TecnySystem.SumbaServices.Sumba.Business.Interfaces;
using TecnySystem.SumbaServices.Sumba.DataAccess.Implementacion;
using TecnySystem.SumbaServices.Sumba.DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Agregar DbContext con MySQL (Oracle)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 22)))); // Asegúrate de usar la versión correcta de tu servidor

builder.Services.AddScoped<IInventarioNeg, InventarioNeg>();
builder.Services.AddScoped<IInventarioDAO, InventarioDAO>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// 🔹 Agregar servicios MVC
builder.Services.AddControllersWithViews();

// 🔹 Configurar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(
        builder.Configuration.GetValue<int>("Session:IdleTimeout", 30)
    );
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



// 🔹 Agregar memoria distribuida para sesiones
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// 🔹 Configurar manejo de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Conexión exitosa a MySQL!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
//.AsSplitQuery() lo divide en multiples consultas 
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    var context = services.GetRequiredService<ApplicationDbContext>();

//    try
//    {
//        var fallasTela = context.FallasTela
//            .AsSplitQuery()
//            .Include(f => f.DescPrenda)
//                .ThenInclude(d => d.Tallas) // 🔹 Incluir tallas relacionadas
//            .Include(f => f.TallasFallaTela) // 🔹 Incluir tallas afectadas en la falla
//            .ToList();


//        if (!fallasTela.Any())
//        {
//            logger.LogWarning("⚠️ No se encontraron fallas de lavandería en la base de datos.");
//        }
//        else
//        {
//            foreach (var falla in fallasTela)
//            {
//                var desc = falla.DescPrenda;

//                logger.LogInformation("-------------------------------------------------");
//                logger.LogInformation("🚨 Falla de Tela (ID: {ID})", falla.id_falla_tela);
//                logger.LogInformation("   🔹 Código Falla: {Codigo}", falla.codigo_falla_tela);
//                logger.LogInformation("   🔹 Descripción: {Descripcion}", falla.descripcion_FTela);
//                logger.LogInformation("   🔹 Estado: {Estado}", falla.estado);

//                // Aquí corregimos para obtener correctamente la cantidad de fallas afectadas por talla
//                var cantidadTotalFallas = falla.TallasFallaTela.Sum(t => t.cantidad_afectada);
//                logger.LogInformation("   🔹 Cantidad total de fallas registradas: {Cantidad}", cantidadTotalFallas);

//                // Información de la prenda asociada
//                if (desc != null)
//                {
//                    logger.LogInformation("   🔹 Código Lote: {CodigoLote}", desc.codigo_lote);
//                    logger.LogInformation("   🔹 Categoría: {Categoria}", desc.categoria);
//                    logger.LogInformation("   🔹 Modelo: {Modelo}", desc.modelo);
//                    logger.LogInformation("   🔹 Color: {Color}", desc.color);
//                    logger.LogInformation("   🔹 Fecha Registro: {Fecha}", desc.fecha_registro.ToString("yyyy-MM-dd"));

//                    // Mostrar tallas afectadas
//                    if (falla.TallasFallaTela != null && falla.TallasFallaTela.Any())
//                    {
//                        logger.LogInformation("   🔹 Tallas afectadas:");
//                        foreach (var talla in falla.TallasFallaTela)
//                        {
//                            logger.LogInformation("       - {Talla}: {Cantidad} unidades afectadas", talla.talla, talla.cantidad_afectada);
//                        }
//                    }
//                    else
//                    {
//                        logger.LogInformation("   🔹 Tallas: No hay tallas registradas.");
//                    }
//                }
//                else
//                {
//                    logger.LogInformation("   🔹 No hay información de la prenda asociada.");
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "❌ Error al obtener fallas de lavandería.");
//    }
//}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        var prendas = context.Prendas
            .AsSplitQuery()
            .Include(p => p.DescPrenda)
                .ThenInclude(d => d.Tallas) // 🔹 Incluir tallas relacionadas
            .ToList();

        if (!prendas.Any())
        {
            Console.WriteLine("⚠️ No se encontraron prendas en la base de datos.");
        }
        else
        {
            foreach (var prenda in prendas)
            {
                var desc = prenda.DescPrenda;

                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"✅ Prenda: {prenda.codigo_prenda}");
                Console.WriteLine($"   🔹 Código Lote: {desc?.codigo_lote ?? "No disponible"}");
                Console.WriteLine($"   🔹 Categoría: {desc?.categoria ?? "No disponible"}");
                Console.WriteLine($"   🔹 Modelo: {desc?.modelo ?? "No disponible"}");
                Console.WriteLine($"   🔹 Color: {desc?.color ?? "No disponible"}");
                Console.WriteLine($"   🔹 Fecha Registro: {desc?.fecha_registro.ToString("yyyy-MM-dd") ?? "No disponible"}");

                // Mostrar las tallas y cantidades disponibles
                if (desc?.Tallas != null && desc.Tallas.Any())
                {
                    Console.WriteLine("   🔹 Tallas disponibles:");
                    foreach (var talla in desc.Tallas)
                    {
                        Console.WriteLine($"       - {talla.talla}: {talla.cantidad} unidades");
                    }
                }
                else
                {
                    Console.WriteLine("   🔹 Tallas: No hay tallas registradas.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al obtener prendas: {ex.Message}");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 🔹 Habilitar sesiones
app.UseSession();
app.UseAuthorization();

// 🔹 Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// 🔹 Ejecutar la aplicación
app.Run();
