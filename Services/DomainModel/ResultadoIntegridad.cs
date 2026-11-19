using System.Collections.Generic;

namespace Services.DomainModel
{
    public class ResultadoIntegridad
    {
        public bool EsValido { get; set; }
        public List<FilaConFalla> FilasConFalla { get; set; } = new List<FilaConFalla>();
    }

    public class FilaConFalla
    {
        public string Tabla { get; set; }
        public string ClavePrimaria { get; set; }
        public string CamposConFalla { get; set; }
    }
}
