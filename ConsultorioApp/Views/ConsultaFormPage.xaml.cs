using ConsultorioApp.Models;

namespace ConsultorioApp.Views;

public partial class ConsultaFormPage : ContentPage {
    public ConsultaFormPage() {
        InitializeComponent();
    }

    private async void OnSalvarClicked(object sender, EventArgs e) {
        var consulta = new Consulta {
            Paciente = PacienteEntry.Text,
            Data = DataPicker.Date,
            Horario = HoraPicker.Time,
            Observacoes = ObsEditor.Text
        };

        await DisplayAlert("Sucesso", $"Consulta agendada para {consulta.Paciente} em {consulta.Data:dd/MM/yyyy} às {consulta.Horario:hh\\:mm}.", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e) {
        await Navigation.PopAsync();
    }
}
