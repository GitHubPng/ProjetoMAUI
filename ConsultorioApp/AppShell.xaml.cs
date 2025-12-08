using ConsultorioApp.Views;

namespace ConsultorioApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rotas para navegação
            Routing.RegisterRoute(nameof(PacienteFormPage), typeof(PacienteFormPage));
            Routing.RegisterRoute(nameof(ConsultaFormPage), typeof(ConsultaFormPage));
        }
    }
}
