using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;
using AyudaHogar.Services; // Asegúrate de incluir el espacio de nombres donde se encuentra UserService
using Microsoft.Extensions.DependencyInjection;
using AyudaHogar.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;

Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AyudahogardbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=tcp:ayudahogarsdb.database.windows.net,1433;Initial Catalog=ayudahogardb;Persist Security Info=False;User ID=ayudahogar;Password=94897563An;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")));

builder.Services.AddSingleton<BlobService>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration["AzureBlobStorage:connectionString"];
    var containerName = configuration["AzureBlobStorage:containerName"];
    return new BlobService(connectionString, containerName);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuración de servicios
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);

        options.Events = new OpenIdConnectEvents
        {
            OnTokenValidated = async ctx =>
            {

                // Obtén el ClaimsPrincipal del usuario autenticado
                var principal = ctx.Principal;

                // Aquí puedes acceder a los claims del usuario, por ejemplo, el nombre de usuario
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;

                // Resuelve el servicio que necesitas para interactuar con tu base de datos
                var userService = ctx.HttpContext.RequestServices.GetRequiredService<UserService>();

                // Verifica si el usuario existe en tu base de datos y obtén su rol
                var user = await userService.VerifyOrAddUserAndGetUser(principal);
                var userRole = user.UsuRol; // Asume que tu servicio devuelve el rol del usuario

                // Agrega el rol del usuario como un claim
                var claimsIdentity = principal.Identity as ClaimsIdentity;
                if (userRole != null && claimsIdentity != null)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userRole));
                }

                // Aquí puedes realizar cualquier otra lógica necesaria con la información del usuario
            }
        };
    });

// Registra UserService en el contenedor de servicios
builder.Services.AddScoped<UserService>();


builder.Services.AddAuthorization(options =>
{
    //options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

// Agrega el servicio de sesión con opciones de configuración (opcional)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Marca la cookie de sesión como HttpOnly
    options.Cookie.IsEssential = true; // Marca la cookie de sesión como esencial
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Asegúrate de llamar a UseSession() después de UseRouting() y antes de UseAuthentication() y UseAuthorization()
app.UseSession();

app.MapRazorPages();
app.MapControllers();

app.Run();
