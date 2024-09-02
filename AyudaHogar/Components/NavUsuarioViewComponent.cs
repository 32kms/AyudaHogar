using Microsoft.AspNetCore.Mvc;
using AyudaHogar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AyudaHogar.Components
{
    public class NavUsuarioViewComponent : ViewComponent
    {
        private readonly AyudahogardbContext _context;

        public NavUsuarioViewComponent(AyudahogardbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categorias = await _context.Categoriadeservicios.ToListAsync();
            return View(categorias);
        }
    }
}