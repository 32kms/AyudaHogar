# AyudaHogar: Encuentra servicios para el hogar de confianza en Chile

## Descripción

AyudaHogar es una plataforma web que facilita la gestión de servicios para el hogar en Chile, conectando a usuarios que necesitan estos servicios con proveedores calificados y confiables. Encuentra fácilmente profesionales para limpieza, reparaciones, jardinería y mucho más. Lee reseñas de otros usuarios, compara servicios y programa citas según tu conveniencia.

## Características Principales

*   **Búsqueda sencilla:** Encuentra proveedores por categoría de servicio (limpieza, reparación, etc.) y ubicación (comuna).
*   **Perfiles detallados de proveedores:** Accede a información completa sobre cada proveedor, incluyendo su experiencia, habilidades, fotos y reseñas de clientes anteriores.
*   **Sistema de reseñas y calificaciones:** Toma decisiones informadas basadas en las experiencias de otros usuarios. Califica a los proveedores y deja comentarios sobre los servicios recibidos.
*   **Gestión de citas:** Programa citas con proveedores según su disponibilidad horaria, simplificando la coordinación.
*   **Autenticación segura:**  Inicia sesión de forma segura con tu cuenta de Google para proteger tu información personal.
*   **Interfaz amigable:**  Nuestro diseño intuitivo facilita la navegación y la búsqueda de servicios.

## Tecnologías Utilizadas

*   **Backend:** ASP.NET Core con .NET 8
*   **Frontend:** HTML, CSS y Bootstrap
*   **Base de datos:** Azure SQL Server
*   **Autenticación:** Azure Active Directory B2C
*   **Almacenamiento:** Azure Blob Storage (para fotos de perfil y reseñas)
*   **Analítica:** Google Analytics

## Instalación

**Nota:** La plataforma está alojada en Azure, por lo que no es necesaria una instalación local. Sin embargo, si deseas ejecutar el código localmente (ten en cuenta que algunas funciones de Azure podrían no estar disponibles), sigue estos pasos:

1.  Clona el repositorio:  `git clone https://github.com/32kms/AyudaHogar.git`
2.  Navega al directorio del proyecto:  `cd AyudaHogar`
3.  Restaura los paquetes de NuGet:  `dotnet restore`
4.  Configura la conexión a la base de datos en  `appsettings.json`.
5.  Ejecuta las migraciones de la base de datos:  `dotnet ef database update`
6.  Inicia la aplicación:  `dotnet run`

## Cómo Contribuir

Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-caracteristica`).
3. Realiza tus cambios y haz commit (`git commit -am 'Añadir nueva característica'`).
4. Sube tus cambios (`git push origin feature/nueva-caracteristica`).
5. Abre un Pull Request.

## Licencia

Este proyecto está bajo la Licencia MIT.

## Agradecimientos

*   A mi profesor guía, Pablo Cerda, por su apoyo y orientación durante el desarrollo de este proyecto.

**¡Gracias por visitar el repositorio de AyudaHogar!**
```
