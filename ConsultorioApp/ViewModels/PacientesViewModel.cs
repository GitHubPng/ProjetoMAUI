using System.Collections.ObjectModel;
using System.Windows.Input;
using ConsultorioApp.Models;
using ConsultorioApp.Services;
using ConsultorioApp.Views;

namespace ConsultorioApp.ViewModels;

public class PacientesViewModel
{
    private readonly PacienteService _service;
    public ObservableCollection<Paciente> Pacientes { get; set; }
    public ICommand NovoPacienteCommand { get; }
    public ICommand EditarPacienteCommand { get; }
    public ICommand ExcluirPacienteCommand { get; }

    public PacientesViewModel(PacienteService service)
    {
        _service = service;
        Pacientes = new ObservableCollection<Paciente>();
        NovoPacienteCommand = new Command(OnNovoPaciente);
        EditarPacienteCommand = new Command<Paciente>(OnEditarPaciente);
        ExcluirPacienteCommand = new Command<Paciente>(OnExcluirPaciente);
        _ = CarregarPacientesAsync();
    }

    private async void OnNovoPaciente()
    {
        await Shell.Current.GoToAsync(nameof(PacienteFormPage));
    }

    private async void OnEditarPaciente(Paciente paciente)
    {
        if (paciente == null) return;
        
        var route = $"{nameof(PacienteFormPage)}?PacienteId={paciente.Id}";
        await Shell.Current.GoToAsync(route);
    }

    private async void OnExcluirPaciente(Paciente paciente)
    {
        if (paciente == null) return;

        bool answer = await Shell.Current.DisplayAlert(
            "Confirmar Exclusão",
            $"Deseja realmente excluir {paciente.Nome}?",
            "Sim", "Não");

        if (answer)
        {
            await _service.DeletePacienteAsync(paciente);
            await CarregarPacientesAsync();
        }
    }

    public async Task CarregarPacientesAsync()
    {
        var lista = await _service.GetPacientesAsync();
        Pacientes.Clear();
        foreach (var paciente in lista)
        {
            Pacientes.Add(paciente);
        }
    }
}
