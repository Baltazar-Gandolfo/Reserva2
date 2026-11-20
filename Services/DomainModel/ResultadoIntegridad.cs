using System.Collections.Generic;

namespace Services.DomainModel
{
    /// <summary>
    /// Resultado de la verificación de integridad de dígitos verificadores.
    /// </summary>
    public class ResultadoIntegridad
    {
        /// <summary>Indica si todos los dígitos verificadores coinciden.</summary>
        public bool EsValido { get; set; }
        /// <summary>Lista de filas cuyo DVH no coincide con el calculado.</summary>
        public List<FilaConFalla> FilasConFalla { get; set; } = new List<FilaConFalla>();
    }

    /// <summary>
    /// Representa una fila con falla de integridad en la verificación DVH.
    /// </summary>
    public class FilaConFalla
    {
        /// <summary>Nombre de la tabla afectada.</summary>
        public string Tabla { get; set; }
        /// <summary>Clave primaria de la fila afectada.</summary>
        public string ClavePrimaria { get; set; }
        /// <summary>Campos que presentan inconsistencia.</summary>
        public string CamposConFalla { get; set; }
    }
}
