using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;
using System.Security.Claims;
using AyudaHogar.Services;

namespace AyudaHogar.Pages
{
    public class MisReportesModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        private readonly UserService _userService;

        public MisReportesModel(AyudaHogar.Models.AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }


        public IList<Report> Report { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userService.AuthenticateUserAndGetUser(User);

            if (user != null && (user.UsuRol == "1" || user.UsuRol == "2"))
            {
                Report = await _context.Reports
                    .Include(r => r.Er)
                    .Include(r => r.Serv)
                    .ThenInclude(s => s.Prov) // Incluye la información del proveedor de servicio
                    .Include(r => r.Usu)
                    .Where(r => r.UsuId == user.UsuId)
                    .ToListAsync();
            }
            else
            {
                // Manejar el caso en que el usuario no tenga el rol adecuado o no esté autenticado
                Report = new List<Report>(); // Devuelve una lista vacía o redirige según sea necesario
            }
        }

    }
}
