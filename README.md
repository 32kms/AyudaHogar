# AyudaHogar

Plataforma web para la gestión de servicios para el hogar en Chile, desarrollada con ASP.NET Core y Azure.

## Descripción

AyudaHogar es una plataforma que facilita la gestión de servicios para el hogar, permitiendo a los usuarios contratar y administrar servicios como limpieza, reparación, entre otros. La plataforma está desarrollada utilizando ASP.NET Core para el backend y Azure para el hosting.

## Proyecto de Seminario de Grado

### Información General
- **Proyecto**: "Desarrollo de una Plataforma Web para Gestión de Servicios para el Hogar"
- **Institución**: Inacap
- **Duración**: Mayo 2024 - Julio 2024

### Tecnologías Utilizadas
- **Backend**: ASP.NET Core con .NET 8
- **Servicios en la Nube**: Azure (Azure Services, Azure AD B2C, Azure App Service, Azure Blob Storage, Azure Storage Account)
- **Base de Datos**: Entity Framework Core
- **Analítica**: Google Analytics

### Descripción del Proyecto
Diseñé y desarrollé una plataforma web para la gestión de servicios para el hogar en Chile. Utilicé servicios de Azure y Google Analytics para recopilar y analizar datos, optimizando la selección de servicios y estableciendo criterios administrativos claros para proveedores.

### Logros
- Desarrollé un prototipo funcional que mejoró la eficiencia en la búsqueda de servicios en un solo lugar.
- Implementé tecnologías avanzadas para asegurar la escalabilidad y seguridad de la plataforma.
- Adapté dinámicamente la plataforma a las necesidades del mercado permitiendo a los usuarios solicitar nuevas categorías de servicios.
- Establecí criterios administrativos para garantizar la calidad y confiabilidad de los proveedores.

## Características

- Gestión de usuarios y autenticación
- Búsqueda y contratación de servicios para el hogar
- Administración de servicios contratados
- Notificaciones y alertas

## Tecnologías

- ASP.NET Core
- Azure
- jQuery
- Bootstrap

## Instalación

1. Clona el repositorio:
    ```bash
    git clone https://github.com/32kms/AyudaHogar.git
    ```
2. Navega al directorio del proyecto:
    ```bash
    cd AyudaHogar
    ```
3. Restaura los paquetes de NuGet:
    ```bash
    dotnet restore
    ```
4. Configura la conexión a la base de datos en `appsettings.json`.
5. Ejecuta las migraciones de la base de datos:
    ```bash
    dotnet ef database update
    ```
6. **Nota Importante**: Los recursos de Azure no funcionarán ya que deben configurarse con sus propias cuentas.
7. Inicia la aplicación:
    ```bash
    dotnet run
    ```

## Contribución

Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-caracteristica`).
3. Realiza tus cambios y haz commit (`git commit -am 'Añadir nueva característica'`).
4. Sube tus cambios (`git push origin feature/nueva-caracteristica`).
5. Abre un Pull Request.

## Licencia

Este proyecto está bajo la Licencia MIT. Puedes ver el archivo de [licencia](https://github.com/32kms/AyudaHogar/blob/main/LICENSE.md) para más detalles.