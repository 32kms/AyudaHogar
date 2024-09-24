# AyudaHogar: Encuentra servicios para el hogar de confianza en Chile

!AyudaHogar Logo

## Descripción

AyudaHogar es una plataforma web que facilita la gestión de servicios para el hogar en Chile, conectando a usuarios que necesitan estos servicios con proveedores calificados y confiables. Encuentra fácilmente profesionales para limpieza, reparaciones, jardinería y mucho más. Lee reseñas de otros usuarios, compara servicios y programa citas según tu conveniencia.

## Características Principales

<ul>
    <li><strong>Búsqueda sencilla:</strong> Encuentra proveedores por categoría de servicio (limpieza, reparación, etc.) y ubicación (comuna).</li>
    <li><strong>Perfiles detallados de proveedores:</strong> Accede a información completa sobre cada proveedor, incluyendo su experiencia, habilidades, fotos y reseñas de clientes anteriores.</li>
    <li><strong>Sistema de reseñas y calificaciones:</strong> Toma decisiones informadas basadas en las experiencias de otros usuarios. Califica a los proveedores y deja comentarios sobre los servicios recibidos.</li>
    <li><strong>Gestión de citas:</strong> Programa citas con proveedores según su disponibilidad horaria, simplificando la coordinación.</li>
    <li><strong>Autenticación segura:</strong> Inicia sesión de forma segura con tu cuenta de Google para proteger tu información personal.</li>
    <li><strong>Interfaz amigable:</strong> Nuestro diseño intuitivo facilita la navegación y la búsqueda de servicios.</li>
</ul>

## Tecnologías Utilizadas

<ul>
    <li><strong>Backend:</strong> ASP.NET Core con .NET 8</li>
    <li><strong>Frontend:</strong> HTML, CSS y Bootstrap</li>
    <li><strong>Base de datos:</strong> Azure SQL Server</li>
    <li><strong>Autenticación:</strong> Azure Active Directory B2C</li>
    <li><strong>Almacenamiento:</strong> Azure Blob Storage (para fotos de perfil y reseñas)</li>
    <li><strong>Analítica:</strong> Google Analytics</li>
</ul>

## Instalación

<p><strong>Nota:</strong> La plataforma está alojada en Azure, por lo que no es necesaria una instalación local. Sin embargo, si deseas ejecutar el código localmente (ten en cuenta que algunas funciones de Azure podrían no estar disponibles), sigue estos pasos:</p>

<ol>
    <li>Clona el repositorio: <code>git clone https://github.com/32kms/AyudaHogar.git</code></li>
    <li>Navega al directorio del proyecto: <code>cd AyudaHogar</code></li>
    <li>Restaura los paquetes de NuGet: <code>dotnet restore</code></li>
    <li>Configura la conexión a la base de datos en <code>appsettings.json</code>.</li>
    <li>Ejecuta las migraciones de la base de datos: <code>dotnet ef database update</code></li>
    <li>Inicia la aplicación: <code>dotnet run</code></li>
</ol>

## Configuración de Servicios en Azure

<p>Para que la aplicación funcione correctamente, es necesario configurar los siguientes servicios en Azure:</p>

<ol>
    <li>Crear un servidor de base de datos en Azure SQL y configurar la cadena de conexión en <code>appsettings.json</code>.</li>
    <li>Configurar Azure Active Directory B2C para la autenticación y agregar la configuración correspondiente en <code>appsettings.json</code>.</li>
    <li>Desplegar la aplicación en Azure App Service.</li>
</ol>

## Cómo Contribuir

<p>Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:</p>

<ol>
    <li>Haz un fork del repositorio.</li>
    <li>Crea una nueva rama (<code>git checkout -b feature/nueva-caracteristica</code>).</li>
    <li>Realiza tus cambios y haz commit (<code>git commit -am 'Añadir nueva característica'</code>).</li>
    <li>Sube tus cambios (<code>git push origin feature/nueva-caracteristica</code>).</li>
    <li>Abre un Pull Request.</li>
</ol>

## Licencia

<p>Este proyecto está bajo la Licencia MIT.</p>

## Agradecimientos

<ul>
    <li>A mi profesor guía, Pablo Cerda, por su apoyo y orientación durante el desarrollo de este proyecto.</li>
</ul>

<p><strong>¡Gracias por visitar el repositorio de AyudaHogar!</strong></p>
