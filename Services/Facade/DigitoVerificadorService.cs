using Services.DomainModel;
using Services.Logic;

namespace Services.Facade
{
    public static class DigitoVerificadorService
    {
        public static ResultadoIntegridad Verificar()
        {
            return DigitoVerificadorLogic.Verificar();
        }

        public static void RecalcularTodo()
        {
            DigitoVerificadorLogic.RecalcularTodo();
        }

        public static bool ExisteDNI(string dniEncriptado)
        {
            return DigitoVerificadorLogic.ExisteDNI(dniEncriptado);
        }
    }
}
