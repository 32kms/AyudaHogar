using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AyudaHogar.Models;
using AyudaHogar.Services;
using System.Security.Claims;

namespace AyudaHogar.Pages
{
    public class SolicitarCategoriaModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;
        private readonly UserService _userService;

        public SolicitarCategoriaModel(AyudaHogar.Models.AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public Solicitudnuevacategorium Solicitudnuevacategorium { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Obtener el usuario conectado a través del servicio UserService
            var user = await _userService.AuthenticateUserAndGetUser(User);

            // Asignar el UsuId del usuario conectado a la solicitud de nueva categoría
            Solicitudnuevacategorium.UsuId = user.UsuId;

            _context.Solicitudnuevacategoria.Add(Solicitudnuevacategorium);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
