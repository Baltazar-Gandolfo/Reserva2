using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        private string password;

        public bool Habilitado
        {
            get { return Estado == "Activo"; }
            set { Estado = value ? "Activo" : "Inactivo"; }
        }

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

        public List<Component> Privilegios { get; set; } = new List<Component>();

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

        public Usuario(string nombre, string email, string password, string rol = "Cliente", string estado = "Activo")
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Rol = rol;
            Estado = estado;
            FechaRegistro = DateTime.Now;
        }

        public Usuario(Guid idUsuario, string nombre, string email, string password, string rol = "Cliente", string estado = "Activo")
            : this(nombre, email, password, rol, estado)
        {
            IdUsuario = idUsuario;
        }

        public Usuario()
        {
        }
    }
}