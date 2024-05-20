El presente proyecto esta construido mediante una arquitectura a capas:

Servicios
Negocio
Datos
Interfaces Comunes y DTO
Esta incluido un proyecto de test unitarios con uno realizado a manera de ejemplo.

La arquitectura propuesta incluye una implementación de un repositorio para la entidad utilizando Entity Framework Core y Dapper para interactuar con la base de datos, de forma tal que permite utilizar en paralelo procedimientos almacenados o Entity Framework y posteriormente manipularse con LinQ.

Este proyecto funciona sobre la versión  de .Net 8.0

Para su ejecución unicamente es necesario actualiar la cadena de conexión conforme al script de bd compartido:
![image](https://github.com/DiegoMadrid404/BibliotecaAPI/assets/71359745/97b0b14f-49ee-4ef0-a448-303516983715)

![image](https://github.com/DiegoMadrid404/BibliotecaAPI/assets/71359745/66209c14-77de-41da-9c76-01a784e10991)


