using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AyudaHogar.Components
{
    public class NavAdministradorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Lista de enlaces para el administrador
            var enlacesAdmin = new List<(string Titulo, string Url)>
            {
                ("Reportes Administrador", "/ReportesAdministrador"),
                ("Categorias de Servicios", "/CategoriasServicio"),
                ("Solicitudes para proveedor de servicio", "/SolicitudesProveedor"),
                ("Solicitudes para Nuevas Categorias", "/ListaDeSolicitudesCategoria"),
                ("Proveedores de servicio", "/ProveedoresAdmin"),
                ("Solicitudes para Proveedores Promocionados", "/SolicitudesPremium")

                // Aquí puedes agregar más enlaces según sea necesario
            };

            return View(enlacesAdmin);
        }
    }
}
