using Context;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<MyDBContext>(
//     options => options.UseMySQL(builder.Configuration.GetConnectionString("MiConnexionMySQL")));

// Cargar variables del archivo .env
Env.Load();

// Cargar las variables de Entorno en unas variablas locales
string? server = Environment.GetEnvironmentVariable("DB_SERVER");
string? db = Environment.GetEnvironmentVariable("DB_NAME");
string? user = Environment.GetEnvironmentVariable("DB_USER");
string? pwd = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Armar la cadena de conexión
string connectionString = $"Server={server};Database={db};User={user};Password='{pwd}';";

builder.Services.AddDbContext<MyDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Mostrar la cadena de conexion
Console.WriteLine($"Connection string: {connectionString}");


// builder.Services.AddDbContext<MyDBContext>(options =>
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("MiConnexionMySQL"),
//         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MiConnexionMySQL"))
//     ));

var app = builder.Build();

// INI -- Comprobar que se puede acceder a la Base de Datos 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDBContext>();

    try
    {
        dbContext.Database.CanConnect(); // Esto ya intenta abrir la conexión
        Console.WriteLine("✅ Conexión exitosa a la base de datos.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error al conectar con la base de datos:");
        Console.WriteLine(ex.Message);
    }
}
// FIN -----------------------------------------------------------------


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
