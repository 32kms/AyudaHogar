using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using AyudaHogar.Services; // Asegúrate de incluir el espacio de nombres para UserService
using System.Security.Claims; // Para acceder a los claims del usuario

namespace AyudaHogar.Pages
{
    public class MisServiciosProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly UserService _userService; // Inyecta el UserService

        public MisServiciosProveedorModel(AyudaHogar.Models.AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IList<Servicio> Servicio { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Obtener el ID del usuario conectado
            var user = await _userService.VerifyOrAddUserAndGetUser(User);
            if (user == null)
            {
                // Manejar el caso en que el usuario no se encuentra o no está conectado
                return;
            }

            // Filtrar el proveedor por el ID del usuario
            var proveedor = await _context.Proveedordeservicios
                .FirstOrDefaultAsync(p => p.UsuId == user.UsuId);

            if (proveedor == null)
            {
                // Manejar el caso en que el usuario no es un proveedor o no tiene servicios asociados
                return;
            }

            // Obtener los servicios del proveedor
            Servicio = await _context.Servicios
                .Include(s => s.Cat)
                .Include(s => s.Comuna)
                .Include(s => s.Prov)
                .Where(s => s.ProvId == proveedor.ProvId)
                .ToListAsync();
        }
    }
}
