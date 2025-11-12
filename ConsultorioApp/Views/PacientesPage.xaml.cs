using ConsultorioApp.Models;
using Microsoft.Maui.Controls;

namespace ConsultorioApp.Views;

public partial class PacientesPage : ContentPage {
    public PacientesPage() {
        InitializeComponent();

        // Exemplo temporário de dados (até conectar ao banco)
        PacientesList.ItemsSource = new List<Paciente>
        {
            new Paciente { Nome = "Maria Silva", Telefone = "(11) 99999-8888" },
            new Paciente { Nome = "João Souza", Telefone = "(11) 98888-7777" }
        };
    }

    private async void OnNovoPacienteClicked(object sender, EventArgs e) {
        await Navigation.PushAsync(new PacienteFormPage());
    }
}
