using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AyudaHogar.Models;

namespace AyudaHogar.Pages
{
    public class VerBloquesdeTiempoProveedorModel : PageModel
    {
        private readonly AyudaHogar.Models.AyudahogardbContext _context;

        public VerBloquesdeTiempoProveedorModel(AyudaHogar.Models.AyudahogardbContext context)
        {
            _context = context;
        }

        public IList<Bloquetiempo> Bloquetiempo { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public async Task OnGetAsync()
        {
            Bloquetiempo = await _context.Bloquetiempos
                .Where(b => b.ServId == id) // Asegúrate de cambiar ServId por Id aquí
                .Include(b => b.Serv)
                .ToListAsync();
        }

        public string DiaSemana(int dia)
        {
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            return dias[dia - 1];
        }

        public async Task<IActionResult> OnPostAsync(int btDia, TimeOnly btHoraInicio, TimeOnly btHoraFin)
        {
            var nuevoBloque = new Bloquetiempo
            {
                BtDia = btDia,
                BtHoraInicio = btHoraInicio,
                BtHoraFin = btHoraFin,
                ServId = id // Asegúrate de que ServId esté disponible y correctamente asignado
            };

            if (!ModelState.IsValid)
            {
                return Page(); // Si el modelo no es válido, simplemente retorna a la página actual sin hacer nada
            }

            _context.Bloquetiempos.Add(nuevoBloque);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = id }); // Redirige a la misma página para mostrar los nuevos datos
        }


    }
}
