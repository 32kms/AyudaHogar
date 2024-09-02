using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AyudaHogar.Models;
using AyudaHogar.Services;
using System.Threading.Tasks;

namespace AyudaHogar.Pages
{
    public class ReportesUsuarioModel : PageModel
    {
        private readonly AyudahogardbContext _context;
        private readonly UserService _userService;

        public ReportesUsuarioModel(AyudahogardbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public int ServId { get; set; }
        public string ServicioNombre { get; set; }

        public IActionResult OnGet(int servId)
        {
            ServId = servId;
            var servicio = _context.Servicios.FirstOrDefault(s => s.ServId == servId);
            ServicioNombre = servicio?.ServDetalle ?? "Desconocido";

            ViewData["ErId"] = new SelectList(_context.Estadoreports, "ErId", "ErNombre");
            ViewData["ServId"] = servId;
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuNom");
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int servId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ahora usamos el servId del parámetro en lugar de confiar en el enlace de modelo
            Report.ServId = servId;

            // Simulamos la obtención del ID del usuario actual
            var userId = await _userService.AuthenticateUserAndGetUser(User).ConfigureAwait(false);
            Report.UsuId = userId.UsuId;
            Report.ErId = 4; // Estado del reporte por defecto

            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ServicioDetalle", new { id = servId });
        }

    }
}
