using System.Collections.ObjectModel;
using System.Windows.Input;
using ConsultorioApp.Models;
using ConsultorioApp.Services;
using ConsultorioApp.Views;

namespace ConsultorioApp.ViewModels;

public class AgendaViewModel
{
    private readonly ConsultaService _service;
    public ObservableCollection<Consulta> Consultas { get; set; }
    public ICommand NovaConsultaCommand { get; }
    public ICommand ExcluirConsultaCommand { get; }

    private DateTime _dataSelecionada;
    public DateTime DataSelecionada
    {
        get => _dataSelecionada;
        set
        {
            _dataSelecionada = value;
            _ = CarregarConsultasDoDiaAsync();
        }
    }

    public AgendaViewModel(ConsultaService service)
    {
        _service = service;
        Consultas = new ObservableCollection<Consulta>();
        NovaConsultaCommand = new Command(OnNovaConsulta);
        ExcluirConsultaCommand = new Command<Consulta>(OnExcluirConsulta);
        _dataSelecionada = DateTime.Today;
        _ = CarregarConsultasDoDiaAsync();
    }

    private async void OnNovaConsulta()
    {
        await Shell.Current.GoToAsync(nameof(ConsultaFormPage));
    }

    private async void OnExcluirConsulta(Consulta consulta)
    {
        if (consulta == null) return;

        bool answer = await Shell.Current.DisplayAlert(
            "Confirmar Exclusão",
            $"Deseja realmente excluir a consulta de {consulta.Paciente}?",
            "Sim", "Não");

        if (answer)
        {
            await _service.DeleteConsultaAsync(consulta);
            await CarregarConsultasDoDiaAsync();
        }
    }

    public async Task CarregarConsultasDoDiaAsync()
    {
        var lista = await _service.GetConsultasByDateAsync(_dataSelecionada);
        Consultas.Clear();
        foreach (var consulta in lista)
        {
            Consultas.Add(consulta);
        }
    }
}
