using Npgsql;
using VueClient.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Activer les enums non mappés globalement (optionnel mais recommandé)
NpgsqlConnection.GlobalTypeMapper.EnableUnmappedTypes();

// Injection du DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        // Mapper explicitement les enums PostgreSQL si nécessaire
        npgsqlOptions.MapEnum<VueClient.Models.Enum.StatutCommande>();
        npgsqlOptions.MapEnum<VueClient.Models.Enum.TypeRetrait>();
        npgsqlOptions.MapEnum<VueClient.Models.Enum.TypeComplement>();
        npgsqlOptions.MapEnum<VueClient.Models.Enum.MoyenPaiement>();
        npgsqlOptions.MapEnum<VueClient.Models.Enum.RoleUser>();
        npgsqlOptions.MapEnum<VueClient.Models.Enum.StatutLivraison>();
    }));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
