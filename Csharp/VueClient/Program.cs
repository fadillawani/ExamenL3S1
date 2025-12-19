using Npgsql;
using VueClient.Data;
using Microsoft.EntityFrameworkCore;
using VueClient.Services;
using VueClient.Services.Impl;
using VueClient.Models.Enum;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Services personnalisés
builder.Services.AddScoped<IPanierServices, PanierServicesImpl>();

// Authentification cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.Cookie.Name = "VueClientAuth";
    });

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// DbContext avec PostgreSQL et mapping d'enums
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.MapEnum<StatutCommande>("statut_commande");
        npgsqlOptions.MapEnum<TypeRetrait>("type_retrait");
        npgsqlOptions.MapEnum<TypeComplement>("type_complement");
        npgsqlOptions.MapEnum<MoyenPaiement>("moyen_paiement");
        npgsqlOptions.MapEnum<RoleUser>("role_user");
        npgsqlOptions.MapEnum<StatutLivraison>("statut_livraison");
    })
);


// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();          // session avant auth
app.UseAuthentication();   // auth avant authorization
app.UseAuthorization();

// Route par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
