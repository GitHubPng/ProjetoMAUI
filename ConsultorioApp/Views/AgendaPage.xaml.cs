using ConsultorioApp.Models;

namespace ConsultorioApp.Views;

public partial class AgendaPage : ContentPage {
    private List<Consulta> TodasConsultas;

    public AgendaPage() {
        InitializeComponent();

        // Dados temporários
        TodasConsultas = new List<Consulta>
        {
            new Consulta { Paciente = "Maria Silva", Data = DateTime.Today, Horario = new TimeSpan(9, 0, 0), Observacoes = "Retorno clínico" },
            new Consulta { Paciente = "João Souza", Data = DateTime.Today, Horario = new TimeSpan(14, 30, 0), Observacoes = "Primeira consulta" },
            new Consulta { Paciente = "Ana Costa", Data = DateTime.Today.AddDays(1), Horario = new TimeSpan(10, 0, 0), Observacoes = "Exames de rotina" }
        };

        AtualizarConsultas(DateTime.Today);
    }

    private void AtualizarConsultas(DateTime data) {
        var consultasDia = TodasConsultas
            .Where(c => c.Data.Date == data.Date)
            .OrderBy(c => c.Horario)
            .ToList();

        ConsultasList.ItemsSource = consultasDia;
    }

    private void OnDataSelecionadaChanged(object sender, DateChangedEventArgs e) {
        AtualizarConsultas(e.NewDate);
    }

    private async void OnNovaConsultaClicked(object sender, EventArgs e) {
        await Navigation.PushAsync(new ConsultaFormPage());
    }
}
