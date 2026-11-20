---
uid: Welcome
title: Reserva2 - Manual del Desarrollador
keywords: Welcome
---
Reserva2 es un sistema de gestión de reservas para comercios, desarrollado en C# con Windows Forms sobre .NET 8 y SQL Server. Este manual documenta la API de cada capa del sistema, generada a partir de los comentarios XML del código fuente.

## Arquitectura

El sistema está organizado en capas, cada una en su propio proyecto:

| Proyecto | Responsabilidad |
|---|---|
| **Reserva2.BE** | Entidades del negocio (Comercio, Sucursal, Reserva, Pago, etc.) y clases auxiliares. |
| **Reserva2.DAL** | Acceso a datos del negocio con Entity Framework (contexto y repositorios). |
| **Services** | Servicios transversales: seguridad (usuarios, familias y patentes), bitácora, registro de errores, criptografía, idiomas, backup y dígitos verificadores. |
| **Reserva2.BLL** | Lógica de negocio: reglas de disponibilidad, reservas, pagos, lista de espera y reportes. |
| **Reserva2.UI** | Interfaz de usuario en Windows Forms. |

## Servicios de seguridad

- **Integridad de datos:** la tabla Usuario cuenta con dígito verificador horizontal (DVH) por fila y vertical (DVV) por tabla. Se recalculan en cada alta, baja o modificación y se verifican al iniciar sesión.
- **Permisos:** modelo de familias y patentes (patrón Composite).
- **Criptografía:** hash de contraseñas y cifrado de datos sensibles como el DNI.
- **Bitácora y errores:** registro de acciones y excepciones del sistema.
- **Multiidioma:** textos traducibles mediante archivos de idioma.
- **Backup y restore** de la base de datos.

## Cómo navegar este manual

Usá el índice de la izquierda: cada namespace agrupa las clases de una capa. Dentro de cada clase se documentan sus propiedades, métodos, parámetros y valores devueltos.

## See Also
**Other Resources**  
[](@VersionHistory)
