using Services.Dal.Implementations;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Logic
{
    internal static class DigitoVerificadorLogic
    {
        private static readonly DigitoVerificadorRepository _repo = new DigitoVerificadorRepository();

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

        public static void RecalcularDVV()
        {
            var filas = _repo.ObtenerUsuariosRaw();
            var dvhs = filas.Select(f => f.ContainsKey("DVH") ? f["DVH"] : "").ToList();
            string dvv = CalcularDVV(dvhs);
            _repo.ActualizarDVV("Usuario", dvv);
        }

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

        public static bool ExisteDNI(string dniEncriptado)
        {
            return _repo.ExisteDNI(dniEncriptado);
        }
    }
}
