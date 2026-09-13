using Services.Facade;
using System;
using System.Windows.Forms;

namespace Reserva2.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.ThreadException += (sender, e) =>
            {
                ErrorService.Registrar(e.Exception, "UI.ThreadException");
                MessageBox.Show(
                    "Ocurrió un error inesperado. El error fue registrado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    ErrorService.Registrar(ex, "AppDomain.UnhandledException");
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.Run(new frmLogin());
        }
    }
}