using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;
using AyudaHogar.Services; // Aseg�rate de incluir el espacio de nombres donde se encuentra UserService
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

// Configuraci�n de servicios
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);

        options.Events = new OpenIdConnectEvents
        {
            OnTokenValidated = async ctx =>
            {

                // Obt�n el ClaimsPrincipal del usuario autenticado
                var principal = ctx.Principal;

                // Aqu� puedes acceder a los claims del usuario, por ejemplo, el nombre de usuario
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;

                // Resuelve el servicio que necesitas para interactuar con tu base de datos
                var userService = ctx.HttpContext.RequestServices.GetRequiredService<UserService>();

                // Verifica si el usuario existe en tu base de datos y obt�n su rol
                var user = await userService.VerifyOrAddUserAndGetUser(principal);
                var userRole = user.UsuRol; // Asume que tu servicio devuelve el rol del usuario

                // Agrega el rol del usuario como un claim
                var claimsIdentity = principal.Identity as ClaimsIdentity;
                if (userRole != null && claimsIdentity != null)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userRole));
                }

                // Aqu� puedes realizar cualquier otra l�gica necesaria con la informaci�n del usuario
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

// Agrega el servicio de sesi�n con opciones de configuraci�n (opcional)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Marca la cookie de sesi�n como HttpOnly
    options.Cookie.IsEssential = true; // Marca la cookie de sesi�n como esencial
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

// Aseg�rate de llamar a UseSession() despu�s de UseRouting() y antes de UseAuthentication() y UseAuthorization()
app.UseSession();

app.MapRazorPages();
app.MapControllers();

app.Run();
