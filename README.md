# 🏟️ Reserva2

> Sistema de gestión de turnos y reservas para comercios deportivos.  
> Canchas de fútbol, pádel, tenis y más — todo en un solo lugar.

**UAI** — Ingeniería de Software 2026  
**Alumno:** Baltazar Gandolfo  
**Legajo:** B00117301-T1

---

## 🏗️ Arquitectura

El sistema implementa una arquitectura de **4 capas** con **2 bases de datos** independientes:

```
                        ┌─────────────────────┐
                        │    Reserva2.UI       │  WinForms MDI (.NET 8)
                        └──────────┬──────────┘
                                   │
                    ┌──────────────┼──────────────┐
                    │              │              │
             ┌──────┴──────┐  ┌───┴───┐  ┌───────┴───────┐
             │ Reserva2.BLL│  │  BE   │  │   Services    │
             │  (negocio)  │  │(DTOs) │  │ (arq. base)   │
             └──────┬──────┘  └───────┘  └───────┬───────┘
                    │                            │
             ┌──────┴──────┐             ┌───────┴───────┐
             │ Reserva2.DAL│             │   ADO.NET     │
             │(Entity Fwk) │             │  (SqlHelper)  │
             └──────┬──────┘             └───────┬───────┘
                    │                            │
            ┌───────┴───────┐           ┌────────┴────────┐
            │ 📦 Reserva2   │           │ 🔒 Reserva2     │
            │   _Negocio    │           │   _Servicio     │
            └───────────────┘           └─────────────────┘
```

| Capa | Proyecto | Tecnología | Base de datos |
|------|----------|------------|---------------|
| Presentación | `Reserva2.UI` | WinForms MDI | — |
| Lógica de negocio | `Reserva2.BLL` | C# | — |
| Acceso a datos (negocio) | `Reserva2.DAL` | Entity Framework | `Reserva2_Negocio` |
| Entidades | `Reserva2.BE` | POCOs | — |
| Arquitectura base | `Services` | ADO.NET | `Reserva2_Servicio` |

---

## 📁 Estructura del proyecto

### 🔒 Services/ — Arquitectura base

Todo lo transversal: seguridad, permisos, auditoría, encriptación, idiomas.

```
Services/
│
├── 📂 DomainModel/
│   ├── 📂 Composite/              🎯 Patrón Composite
│   │   ├── Component.cs           Clase abstracta base
│   │   ├── Familia.cs             Nodo — agrupa permisos (= Rol)
│   │   ├── Patente.cs             Hoja — permiso atómico (= pantalla)
│   │   └── Usuario.cs             Usuario con lista de privilegios Composite
│   ├── LogEntry.cs                Entidad de bitácora (auditoría)
│   ├── FileLogEntry.cs            Entidad del logger a archivo
│   └── LogLevel.cs                Enum: Debug, Info, Warn, Error, Fatal
│
├── 📂 DataAccess/
│   ├── 📂 Tools/
│   │   └── SqlHelper.cs           🔧 Ejecuta SQL puro (NonQuery, Scalar, Reader)
│   ├── 📂 Interfaces/             Contratos de repositorios
│   ├── 📂 Implementations/
│   │   ├── 📂 Adapters/           Mapean filas SQL → objetos C#
│   │   ├── UsuarioRepository.cs   CRUD de usuarios
│   │   ├── FamiliaRepository.cs   CRUD de familias (roles)
│   │   ├── PatenteRepository.cs   CRUD de patentes (permisos)
│   │   ├── Familia*.cs            Tablas intermedias N:M del Composite
│   │   ├── Usuario*.cs            Tablas intermedias N:M usuario-permisos
│   │   ├── BitacoraRepository.cs  Auditoría en BD
│   │   ├── ErrorRepository.cs     Log de errores en BD
│   │   └── BackupRepository.cs    BACKUP/RESTORE de SQL Server
│   └── IdiomaDal.cs               Lee archivos de traducción (.es-AR, .en-US)
│
├── 📂 Logic/
│   ├── UsuarioLogic.cs            Validación de credenciales
│   ├── BitacoraLogic.cs           Registra acciones con usuario de sesión
│   ├── IdiomaLogic.cs             Motor de traducción por cultura
│   ├── 📂 Infraestructure/
│   │   └── CryptographyLogic.cs   🔐 MD5 (passwords) + AES-256 (datos sensibles)
│   └── 📂 CustomExceptions/
│       └── PalabraNoEncontradaException.cs
│
└── 📂 Facade/                     🎭 Fachada pública
    ├── LoginService.cs            Autenticación
    ├── SessionManager.cs          🔁 Singleton — sesión activa
    ├── UsuarioService.cs          Operaciones de usuario
    ├── FamiliaService.cs          Gestión de familias
    ├── PatenteService.cs          Gestión de patentes
    ├── BitacoraService.cs         Auditoría
    ├── ErrorService.cs            Manejo de errores (BD + fallback JSON)
    ├── BackupService.cs           Backup y restauración
    ├── CryptographyService.cs     Encriptación
    └── IdiomaService.cs           Multiidioma
```

### 📦 Reserva2.BE/ — Entidades de negocio

Clases POCO sin lógica — solo propiedades que representan las tablas de `Reserva2_Negocio`.

```
Entities/
├── Comercio.cs                 Comercio registrado en la plataforma
├── ElementoReservable.cs       Cancha, pista u otro ítem reservable
├── Reserva.cs                  Reserva de un slot por un cliente
├── Disponibilidad.cs           Slot horario (fecha + hora + precio + estado)
├── ReglaDisponibilidad.cs      Regla que genera slots automáticamente
├── Pago.cs                     Registro de pago manual
├── Sena.cs                     Seña parcial de una reserva
├── CBU.cs                      Datos bancarios del comercio
├── ReglaNegocio.cs             Configuración por comercio (%, vencimiento)
├── ListaEspera.cs              Cola de espera para slots ocupados
├── Notificacion.cs             Notificación al usuario
└── EstadisticaDiaria.cs        Métricas diarias por elemento
```

### 🖥️ Reserva2.UI/ — Interfaz

```
├── Program.cs                  Punto de entrada + captura global de excepciones
├── frmLogin.cs                 🔑 Login con validación contra BD
├── App.config                  Connection strings + config I18n
│
├── 📂 Controles/
│   ├── frmPrincipal.cs         MDI padre, menú dinámico por rol + idioma
│   ├── frmRecuperarPass.cs     Recuperar contraseña (pendiente)
│   └── frmRegistro.cs          Registro de usuario (pendiente)
│
├── 📂 Usuarios/
│   ├── frmUsuarios.cs          Listado de usuarios (pendiente)
│   ├── frmPermisos.cs          🌳 TreeView Composite — gestión de permisos
│   └── frmDashboard.cs         Dashboard (pendiente)
│
├── 📂 Comercios/               Gestión de comercios (pendiente)
├── 📂 Elementos/               ABM de elementos reservables (pendiente)
├── 📂 Disponibilidad/          Configuración de horarios (pendiente)
├── 📂 Reservas/                Flujo de reservas (pendiente)
├── 📂 Pagos/                   Registro de pagos (pendiente)
│
├── 📂 Auditoria/
│   └── frmAuditoria.cs         📋 Consulta de bitácora + filtros + export CSV
│
├── 📂 Config/
│   └── frmBackup.cs            💾 Backup y restauración de ambas BD
│
└── 📂 I18n/                    🌐 Archivos de traducción
    ├── idioma.es-AR            Español (Argentina)
    └── idioma.en-US            English (US)
```

---

## 🎨 Patrones de diseño

| Patrón | Implementación | Propósito |
|--------|---------------|-----------|
| 🔁 **Singleton** | `SessionManager` | Una sola sesión activa en toda la aplicación |
| 🌳 **Composite** | `Component` → `Familia` + `Patente` | Árbol recursivo de permisos asignables a usuarios |
| 🎭 **Facade** | `Services/Facade/*` | Punto de entrada único y simple para cada módulo |
| 🔌 **Adapter** | `*Adapter.cs` | Transforma filas de BD en objetos del dominio |
| 👁️ **Observer** | Sistema de idiomas | Los menús se actualizan al cambiar de idioma |
| 📦 **Repository** | `*Repository.cs` | Abstrae el acceso a datos detrás de una interfaz |

---

## 🗄️ Bases de datos

### Reserva2_Servicio
> Seguridad y operación transversal

`Usuario` · `Sesion` · `Familia` · `Patente` · `FamiliaPatente` · `FamiliaFamilia` · `UsuarioFamilia` · `UsuarioPatente` · `Bitacora` · `LogError` · `DigitoVerificadorVertical`

### Reserva2_Negocio
> Lógica de negocio

`Comercio` · `ElementoReservable` · `ReglaDisponibilidad` · `Disponibilidad` · `Reserva` · `Pago` · `Sena` · `CBU` · `ReglaNegocio` · `ListaEspera` · `Notificacion` · `EstadisticaDiaria` · `DigitoVerificadorVertical`

---

## ✅ Estado de funcionalidades

### Arquitectura base
- [x] Login / Logout con sesión Singleton
- [x] Menú filtrado por rol (Admin / Comercio / Cliente)
- [x] Gestión de permisos — Composite (Familias + Patentes + TreeView)
- [x] Bitácora de auditoría (registrar + consultar + exportar CSV)
- [x] Backup y restauración de ambas bases de datos
- [x] Manejo global de errores (BD + fallback a archivo JSON)
- [x] Encriptación — MD5 para passwords, AES-256 para datos sensibles
- [x] Múltiples idiomas (es-AR, en-US) con cambio en caliente

### Negocio
- [ ] Gestión de comercios
- [ ] Gestión de elementos reservables
- [ ] Configuración de disponibilidad
- [ ] Flujo de reservas
- [ ] Registro de pagos

---

## 🚀 Cómo ejecutar

1. Abrir `Reserva2.sln` en **Visual Studio 2022+**
2. Ejecutar los scripts SQL en SSMS (en este orden):
   - `SQL_Servicio.sql`
   - `SQL_Negocio.sql`
   - `SQL_TablaComposite.sql`
3. Ajustar `Data Source` en `Reserva2.UI/App.config` según tu instancia SQL
4. Click derecho en **Reserva2.UI** → Establecer como proyecto de inicio
5. **F5** para ejecutar
6. Login: `admin@reserva2.com` / `admin123`

---

## 🛠️ Tecnologías

- .NET 8.0
- Windows Forms (WinForms MDI)
- ADO.NET (arquitectura base)
- Entity Framework Core (negocio — próxima iteración)
- SQL Server 2019+

---

<p align="center">
  <sub>Desarrollado por Baltazar Gandolfo · UAI 2026</sub>
</p>
