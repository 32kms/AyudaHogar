using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AyudaHogar.Components
{
    public class NavRolDosViewComponent : ViewComponent
    {
        public NavRolDosViewComponent()
        {
            // Inicialización si es necesario
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Aquí puedes agregar lógica para determinar qué mostrar, si es necesario
            return View();
        }
    }
}
