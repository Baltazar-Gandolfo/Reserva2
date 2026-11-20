using Services.DomainModel;
using Services.Logic;

namespace Services.Facade
{
    /// <summary>
    /// Fachada para el servicio de dígitos verificadores (DVH y DVV).
    /// </summary>
    public static class DigitoVerificadorService
    {
        /// <summary>
        /// Verifica la integridad de los datos comparando DVH y DVV almacenados contra los calculados.
        /// </summary>
        /// <returns>Resultado indicando si la integridad es válida y las filas con fallas.</returns>
        public static ResultadoIntegridad Verificar()
        {
            return DigitoVerificadorLogic.Verificar();
        }

        /// <summary>
        /// Recalcula todos los DVH de cada fila y el DVV de la tabla Usuario.
        /// </summary>
        public static void RecalcularTodo()
        {
            DigitoVerificadorLogic.RecalcularTodo();
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el DNI encriptado indicado.
        /// </summary>
        /// <param name="dniEncriptado">DNI encriptado a verificar.</param>
        /// <returns><c>true</c> si el DNI ya existe en la base de datos.</returns>
        public static bool ExisteDNI(string dniEncriptado)
        {
            return DigitoVerificadorLogic.ExisteDNI(dniEncriptado);
        }
    }
}
