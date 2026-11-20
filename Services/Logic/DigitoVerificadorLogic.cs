using Services.Dal.Implementations;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Logic
{
    /// <summary>
    /// Lógica de cálculo y verificación de dígitos verificadores horizontales (DVH) y verticales (DVV).
    /// </summary>
    internal static class DigitoVerificadorLogic
    {
        private static readonly DigitoVerificadorRepository _repo = new DigitoVerificadorRepository();

        /// <summary>
        /// Calcula el dígito verificador horizontal (DVH) para una fila de la tabla Usuario.
        /// </summary>
        /// <param name="campos">Diccionario con los valores de los campos de la fila.</param>
        /// <returns>DVH calculado como cadena numérica.</returns>
        public static string CalcularDVH(Dictionary<string, string> campos)
        {
            string[] orden = { "IdUsuario", "DNI", "Nombre", "Email", "PasswordHash", "Rol", "Telefono", "Estado", "FechaRegistro" };
            long acumulado = 0;
            for (int c = 0; c < orden.Length; c++)
            {
                string valor = campos.ContainsKey(orden[c]) ? campos[orden[c]] ?? "" : "";
                for (int i = 0; i < valor.Length; i++)
                {
                    acumulado += (long)valor[i] * (i + 1) * (c + 1);
                }
            }
            return acumulado.ToString();
        }

        /// <summary>
        /// Calcula el dígito verificador vertical (DVV) como suma de todos los DVH.
        /// </summary>
        /// <param name="dvhs">Lista de DVH de cada fila.</param>
        /// <returns>DVV calculado como cadena numérica.</returns>
        public static string CalcularDVV(List<string> dvhs)
        {
            long acumulado = 0;
            foreach (string dvh in dvhs)
            {
                if (long.TryParse(dvh, out long valor))
                    acumulado += valor;
            }
            return acumulado.ToString();
        }

        /// <summary>
        /// Recalcula el DVH de cada fila y el DVV de la tabla Usuario completa.
        /// </summary>
        public static void RecalcularTodo()
        {
            var filas = _repo.ObtenerUsuariosRaw();
            var dvhs = new List<string>();

            foreach (var fila in filas)
            {
                string dvh = CalcularDVH(fila);
                Guid id = Guid.Parse(fila["IdUsuario"]);
                _repo.ActualizarDVH(id, dvh);
                dvhs.Add(dvh);
            }

            string dvv = CalcularDVV(dvhs);
            _repo.ActualizarDVV("Usuario", dvv);
        }

        /// <summary>
        /// Recalcula solo el DVV a partir de los DVH almacenados en la base de datos.
        /// </summary>
        public static void RecalcularDVV()
        {
            var filas = _repo.ObtenerUsuariosRaw();
            var dvhs = filas.Select(f => f.ContainsKey("DVH") ? f["DVH"] : "").ToList();
            string dvv = CalcularDVV(dvhs);
            _repo.ActualizarDVV("Usuario", dvv);
        }

        /// <summary>
        /// Verifica la integridad de DVH y DVV comparando valores almacenados contra los calculados.
        /// </summary>
        /// <returns>Resultado con las filas que presentan inconsistencias.</returns>
        public static ResultadoIntegridad Verificar()
        {
            var resultado = new ResultadoIntegridad { EsValido = true };
            var filas = _repo.ObtenerUsuariosRaw();
            var dvhsAlmacenados = new List<string>();

            foreach (var fila in filas)
            {
                string dvhCalculado = CalcularDVH(fila);
                string dvhAlmacenado = fila.ContainsKey("DVH") ? fila["DVH"] : "";
                dvhsAlmacenados.Add(dvhAlmacenado);

                if (dvhCalculado != dvhAlmacenado)
                {
                    resultado.EsValido = false;
                    resultado.FilasConFalla.Add(new FilaConFalla
                    {
                        Tabla = "Usuario",
                        ClavePrimaria = fila["IdUsuario"],
                        CamposConFalla = "DVH"
                    });
                }
            }

            string dvvCalculado = CalcularDVV(dvhsAlmacenados);
            string dvvAlmacenado = _repo.ObtenerDVV("Usuario");

            if (dvvCalculado != dvvAlmacenado)
            {
                resultado.EsValido = false;
                bool yaExiste = resultado.FilasConFalla.Exists(f => f.CamposConFalla == "DVV");
                if (!yaExiste)
                {
                    resultado.FilasConFalla.Add(new FilaConFalla
                    {
                        Tabla = "Usuario",
                        ClavePrimaria = "(tabla completa)",
                        CamposConFalla = "DVV"
                    });
                }
            }

            return resultado;
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el DNI encriptado indicado.
        /// </summary>
        /// <param name="dniEncriptado">DNI encriptado a buscar.</param>
        /// <returns><c>true</c> si el DNI ya existe.</returns>
        public static bool ExisteDNI(string dniEncriptado)
        {
            return _repo.ExisteDNI(dniEncriptado);
        }
    }
}
