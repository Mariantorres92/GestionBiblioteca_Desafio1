\# Sistema de Gestion de Biblioteca

\### Desafio 1 - Programacion Orientada a Objetos



\## Descripcion

Sistema de escritorio desarrollado en C# Windows Forms (.NET Framework 4.7.2) para gestionar libros, usuarios y prestamos de una biblioteca. Aplica los principios de la Programacion Orientada a Objetos.



\## Principios POO Aplicados



\- \*\*Abstraccion\*\*: Clase abstracta MaterialBiblioteca como base del dominio

\- \*\*Herencia\*\*: Libro hereda de MaterialBiblioteca

\- \*\*Encapsulamiento\*\*: Propiedades con getters/setters y logica privada en servicios

\- \*\*Polimorfismo\*\*: Metodo ObtenerDescripcion() sobrescrito en cada clase hija



\## Estructuras de Datos Utilizadas



\- \*\*Diccionarios\*\*: Para almacenar materiales y usuarios por ID

\- \*\*Listas\*\*: Para gestionar prestamos

\- \*\*LINQ\*\*: Para consultas y estadisticas



\## Estructura del Proyecto

```

GestionBiblioteca\_Desafio1/

├── Domain/

│   ├── MaterialBiblioteca.cs   # Clase abstracta base

│   ├── Libro.cs                # Hereda de MaterialBiblioteca

│   ├── UsuarioBiblioteca.cs    # Modelo de usuario

│   └── Prestamo.cs             # Modelo de prestamo

├── Services/

│   └── BibliotecaService.cs    # Logica de negocio (CRUD)

├── Forms/

│   ├── FormLibros.cs           # CRUD de libros

│   ├── FormUsuarios.cs         # CRUD de usuarios

│   ├── FormPrestamos.cs        # Gestion de prestamos

│   └── FormEstadisticas.cs     # Graficas estadisticas

└── Form1.cs                    # Formulario principal

```



\## Instalacion



1\. Clonar el repositorio:

```

git clone https://github.com/Mariantorres92/GestionBiblioteca\_Desafio1.git

```



2\. Abrir GestionBiblioteca\_Desafio1.sln en Visual Studio 2019 o superior



3\. Verificar que el proyecto usa .NET Framework 4.7.2



4\. Compilar con Ctrl+Shift+B



5\. Ejecutar con F5



\## Uso



\### Modulo de Libros

\- Agregar, editar y eliminar libros

\- Buscar por titulo o autor

\- Ver disponibilidad de cada libro



\### Modulo de Usuarios

\- Registrar y gestionar usuarios

\- Ver prestamos activos por usuario



\### Modulo de Prestamos

\- Registrar prestamos y devoluciones

\- Filtrar por activos o todos

\- Control de disponibilidad automatico



\### Modulo de Estadisticas

\- Top libros mas prestados

\- Usuarios mas activos

\- Estado general de prestamos en grafica circular



\## Tecnologias



\- C# .NET Framework 4.7.2

\- Windows Forms

\- System.Windows.Forms.DataVisualization



\## Autora



Mariantorres92

Desafio 1 - Programacion Orientada a Objetos

