using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TecnySystem.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Agregar DbContext con MySQL (Oracle)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MariaDbServerVersion(new Version(10, 5, 9)))); // Asegúrate de usar la versión correcta de tu servidor



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
        Console.WriteLine("✅ Conexión exitosa a MySQL!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error: " + ex.Message);
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
