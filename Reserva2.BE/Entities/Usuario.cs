using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva2.BE.Entities
{
    /// <summary>
    /// Representa un usuario del sistema con sus datos personales y credenciales.
    /// </summary>
    public class Usuario
    {
        /// <summary>Identificador único del usuario.</summary>
        public int IdUsuario { get; set; }
        /// <summary>Documento Nacional de Identidad (almacenado cifrado).</summary>
        public string DNI { get; set; }
        /// <summary>Nombre completo del usuario.</summary>
        public string Nombre { get; set; }
        /// <summary>Dirección de correo electrónico.</summary>
        public string Email { get; set; }
        /// <summary>Hash de la contraseña del usuario.</summary>
        public string PasswordHash { get; set; }
        /// <summary>Rol del usuario (Administrador, Comercio, Cliente).</summary>
        public string Rol { get; set; }
        /// <summary>Número de teléfono de contacto.</summary>
        public string Telefono { get; set; }
        /// <summary>Estado de la cuenta (Activo, Inactivo, Bloqueado).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha de registro del usuario en el sistema.</summary>
        public DateTime FechaRegistro { get; set; }
    }
}
