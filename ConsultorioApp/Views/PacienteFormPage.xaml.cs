using ConsultorioApp.Models;
using Microsoft.Maui.Controls;

namespace ConsultorioApp.Views;

public partial class PacienteFormPage : ContentPage {
    public PacienteFormPage() {
        InitializeComponent();
    }

    private async void OnSalvarClicked(object sender, EventArgs e) {
        // Monta objeto Paciente
        var paciente = new Paciente {
            Nome = NomeEntry.Text,
            DataNascimento = NascimentoPicker.Date,
            Telefone = TelefoneEntry.Text,
            Email = EmailEntry.Text,
            // Adiciona observações (opcionalmente poderia criar uma propriedade no model)
        };

        // Aqui futuramente salvaremos no banco (SQLite)
        await DisplayAlert("Sucesso", $"Paciente {paciente.Nome} cadastrado com sucesso!", "OK");

        // Retorna para a lista
        await Navigation.PopAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e) {
        await Navigation.PopAsync();
    }
}

