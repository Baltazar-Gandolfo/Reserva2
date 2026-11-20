using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// Entidad de dominio que representa un usuario del sistema con sus privilegios.
    /// </summary>
    public class Usuario
    {
        /// <summary>Identificador único del usuario.</summary>
        public Guid IdUsuario { get; set; }
        /// <summary>Documento Nacional de Identidad (almacenado cifrado).</summary>
        public string DNI { get; set; }
        /// <summary>Nombre completo del usuario.</summary>
        public string Nombre { get; set; }
        /// <summary>Dirección de correo electrónico.</summary>
        public string Email { get; set; }
        /// <summary>Número de teléfono de contacto.</summary>
        public string Telefono { get; set; }
        /// <summary>Rol del usuario (Admin, Comercio, Cliente).</summary>
        public string Rol { get; set; }
        /// <summary>Estado del usuario (Activo, Inactivo, Bloqueado).</summary>
        public string Estado { get; set; }
        /// <summary>Fecha en que se registró el usuario.</summary>
        public DateTime FechaRegistro { get; set; }

        private string password;

        /// <summary>Indica si el usuario está habilitado (Estado == "Activo").</summary>
        public bool Habilitado
        {
            get { return Estado == "Activo"; }
            set { Estado = value ? "Activo" : "Inactivo"; }
        }

        /// <summary>Hash de la contraseña del usuario.</summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>Lista de privilegios asignados al usuario (familias y patentes).</summary>
        public List<Component> Privilegios { get; set; } = new List<Component>();

        /// <summary>Obtiene todas las patentes del usuario, recorriendo la jerarquía de familias.</summary>
        public List<Patente> Patentes
        {
            get
            {
                List<Patente> patentes = new List<Patente>();
                RecorrerFamilias(patentes, Privilegios);
                return patentes;
            }
        }

        private void RecorrerFamilias(List<Patente> patentes, List<Component> componentes)
        {
            foreach (var componente in componentes)
            {
                if (componente is Patente patente)
                {
                    if (!patentes.Exists(p => p.Id == patente.Id))
                        patentes.Add(patente);
                }
                else if (componente is Familia familia)
                {
                    RecorrerFamilias(patentes, familia.GetHijos());
                }
            }
        }

        /// <summary>Obtiene todas las familias asignadas al usuario, incluyendo las anidadas.</summary>
        public List<Familia> Familias
        {
            get
            {
                List<Familia> familias = new List<Familia>();
                RecorrerFamilias(familias, Privilegios);
                return familias;
            }
        }

        private void RecorrerFamilias(List<Familia> familias, List<Component> componentes)
        {
            foreach (var componente in componentes)
            {
                if (componente is Familia familia)
                {
                    if (!familias.Exists(p => p.Id == familia.Id))
                        familias.Add(familia);
                    RecorrerFamilias(familias, familia.GetHijos());
                }
            }
        }

        /// <summary>
        /// Crea un nuevo usuario con los datos especificados.
        /// </summary>
        /// <param name="nombre">Nombre completo.</param>
        /// <param name="email">Email.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="rol">Rol (por defecto Cliente).</param>
        /// <param name="estado">Estado (por defecto Activo).</param>
        public Usuario(string nombre, string email, string password, string rol = "Cliente", string estado = "Activo")
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Rol = rol;
            Estado = estado;
            FechaRegistro = DateTime.Now;
        }

        /// <summary>
        /// Crea un usuario existente con su identificador.
        /// </summary>
        /// <param name="idUsuario">Identificador único.</param>
        /// <param name="nombre">Nombre completo.</param>
        /// <param name="email">Email.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="rol">Rol.</param>
        /// <param name="estado">Estado.</param>
        public Usuario(Guid idUsuario, string nombre, string email, string password, string rol = "Cliente", string estado = "Activo")
            : this(nombre, email, password, rol, estado)
        {
            IdUsuario = idUsuario;
        }

        /// <summary>Inicializa una nueva instancia vacía de <see cref="Usuario"/>.</summary>
        public Usuario()
        {
        }
    }
}