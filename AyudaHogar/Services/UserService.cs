using AyudaHogar.Models; // Asegúrate de usar el espacio de nombres correcto para tus modelos
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AyudaHogar.Services
{
    public class UserService
    {
        private readonly AyudahogardbContext _context;

        public UserService(AyudahogardbContext context)
        {
            _context = context;
        }

        public async Task<string> VerifyOrAddUserAndGetRole(string userId)
        {
            // Verificar si el usuario ya existe en la base de datos
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.AzureAduserId == userId);

            if (user == null)
            {
                // Si el usuario no existe, agregarlo y asignarle un rol por defecto
                user = new Usuario
                {
                    AzureAduserId = userId,
                    // Configura las propiedades adicionales según sea necesario
                    UsuRol = "1" // Rol por defecto
                };
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
            }

            // Devolver el rol del usuario
            return user.UsuRol;
        }


        public async Task<Usuario> VerifyOrAddUserAndGetUser(ClaimsPrincipal principal)
        {
            // Cambia "sub" por el URI del claim de Azure AD
            var userId = principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("El ID de usuario de Azure AD no está disponible.");
            }
            // Ajuste para manejar el claim "emails" como una lista y extraer el primer correo electrónico
            var emailClaim = principal.FindFirst("emails")?.Value ?? principal.FindFirst(ClaimTypes.Email)?.Value;
            string email;

            // Intenta parsear el claim como un array JSON solo si parece ser un array JSON
            if (!string.IsNullOrEmpty(emailClaim) && emailClaim.StartsWith("[") && emailClaim.EndsWith("]"))
            {
                try
                {
                    email = Newtonsoft.Json.Linq.JArray.Parse(emailClaim).First.ToString();
                }
                catch (Newtonsoft.Json.JsonReaderException)
                {
                    // Manejar el error o registrar el problema
                    email = null; // Asegúrate de asignar un valor por defecto o manejar este caso adecuadamente
                }
            }
            else
            {
                // Si el claim no parece ser un array JSON, asume que es un único correo electrónico
                email = emailClaim;
            }
            var firstName = principal.FindFirst("given_name")?.Value ?? principal.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = principal.FindFirst("family_name")?.Value ?? principal.FindFirst(ClaimTypes.Surname)?.Value;

            if (userId == null)
            {
                throw new Exception("El ID de usuario de Azure AD B2C no está disponible.");
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.AzureAduserId == userId);

            if (user == null)
            {
                user = new Usuario
                {
                    AzureAduserId = userId,
                    UsuNom = firstName,
                    UsuApe = lastName,
                    UsuMail = email,
                    // Los siguientes campos se dejan como null según las instrucciones
                    UsuFotoPerfilUrl = null,
                    UsuPhone = null,
                    UsuRol = "1"// Rol por defecto
                };

                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
        public async Task<Usuario> AuthenticateUserAndGetUser(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("El ID de usuario de Azure AD no está disponible.");
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.AzureAduserId == userId);

            if (user == null)
            {
                // Opción 1: Crear y devolver un nuevo usuario (descomenta el siguiente bloque para usar esta opción)
                /*
                user = new Usuario
                {
                    AzureAduserId = userId,
                    // Configura las propiedades adicionales según sea necesario
                };
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
                */

                // Opción 2: Devolver null o lanzar una excepción si prefieres no crear un usuario automáticamente
                // return null;
                throw new Exception("Usuario no encontrado y la creación automática está deshabilitada.");
            }

            return user;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

    }
}
