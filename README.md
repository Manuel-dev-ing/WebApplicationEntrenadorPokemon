# Entrenador Pokemon

## Descripcion
Este proyecto es una aplicación web diseñada para consultar, guardar y gestionar información de Pokémon e ítems en una interfaz accesible y fácil de navegar. Desarrollada en ASP.NET Core (.NET 8) con Entity Framework Core y SQL Server como base de datos, esta aplicación utiliza la PokeAPI para proporcionar datos en tiempo real sobre Pokémon e ítems de la franquicia. Además, se ha integrado JavaScript para mejorar la experiencia de usuario en el lado del cliente.

## Funcionalidades

- `Exploración de la Pokédex Nacional:` La Pokédex Nacional incluye a los primeros 151 Pokémon, con información detallada sobre cada uno. Los usuarios pueden explorar la lista, ver los tipos de cada Pokémon, sus estadísticas, y acceder a detalles específicos como movimientos y habilidades.

- `Catálogo de Ítems:` La aplicación muestra un catálogo de ítems disponibles en la PokeAPI, con información útil y descriptiva sobre cada uno. Los usuarios pueden visualizar detalles de cada ítem, como sus efectos y tipos, en un formato claro y organizado.

- `Búsqueda por Tipo de Pokémon:` Para facilitar la exploración, los usuarios pueden buscar Pokémon por tipo (por ejemplo: Agua, Fuego, Planta). Esto permite una experiencia de búsqueda más rápida y específica.

- `Pokémon Favoritos(Mis Pokemon):` Los usuarios pueden marcar hasta 6 Pokémon como favoritos para crear su equipo personalizado. Además, pueden eliminar Pokémon de su lista de favoritos en cualquier momento para ajustar su equipo según sus preferencias.

- `Ítems Guardados(Mis Items):` Además de Pokémon, los usuarios tienen la opción de guardar ítems en su perfil, lo que les permite crear una colección personalizada. Esta funcionalidad es ideal para aquellos que buscan tener a mano ítems de utilidad frecuente.

## Arquitectura de la Aplicación
Para garantizar un código modular, fácil de mantener y testear, el proyecto emplea el patrón de diseño Repository. Este patrón separa la lógica de acceso a datos de la lógica de negocio, proporcionando una capa de abstracción entre la base de datos y el resto de la aplicación.

El uso del patrón Repository facilita la gestión de las operaciones CRUD de las entidades (como Pokémon y ítems), permitiendo a los desarrolladores realizar cambios en la capa de datos sin afectar otras partes del sistema. Esto es especialmente útil en aplicaciones de gran escala, donde el mantenimiento y la extensibilidad son críticos.

La arquitectura de esta aplicación está basada en el modelo MVC (Modelo-Vista-Controlador), que organiza el código en capas bien definidas para una mayor claridad y mantenimiento.

1. Modelo: Entity Framework Core facilita la comunicación entre la aplicación y SQL Server mediante modelos de datos.
2. Controlador: ASP.NET Core maneja la lógica de negocio, haciendo peticiones a la PokeAPI y gestionando los datos en la base de datos.
3. Vista: Las vistas son dinámicas y responsivas, creadas con HTML, CSS y JavaScript, lo que proporciona una experiencia de usuario interactiva y fluida.

## Configuración y Ejecución del Proyecto

1. Clona el repositorio en tu máquina local.
2. Configura la conexión a SQL Server en el archivo de configuración de la aplicación (appsettings.json) para asegurarte de que la base de datos esté correctamente enlazada.
3. Ejecuta las migraciones de Entity Framework Core mediante el comando:

  ```
   dotnet ef database update

 ```
   Esto generará la estructura de la base de datos necesaria para el proyecto.

4. Ejecuta el proyecto desde Visual Studio o mediante dotnet run desde la terminal.
## Licencia

Entrenador Pokemon es [MIT licensed](./LICENSE).

## Contacto
**Nombre:** Manuel Tamayo Montero.

**Telefono:** +52 4251324916

**Correo:** manueltamayo9765@gmail.com

  


