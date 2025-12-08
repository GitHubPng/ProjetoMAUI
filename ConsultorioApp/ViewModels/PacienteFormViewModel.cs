using ConsultorioApp.Models;
using ConsultorioApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsultorioApp.ViewModels;

public class PacienteFormViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly PacienteService _service;
    
    private Paciente _paciente;
    public Paciente Paciente
    {
        get => _paciente;
        set
        {
            _paciente = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public PacienteFormViewModel(PacienteService service)
    {
        _service = service;
        Paciente = new Paciente { DataNascimento = DateTime.Today.AddYears(-18) };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("PacienteId"))
        {
            var id = int.Parse(query["PacienteId"].ToString()!);
            _ = CarregarPacienteAsync(id);
        }
    }

    private async Task CarregarPacienteAsync(int id)
    {
        var paciente = await _service.GetPacienteAsync(id);
        if (paciente != null)
            Paciente = paciente;
    }

    public async Task<bool> SalvarAsync()
    {
        if (string.IsNullOrWhiteSpace(Paciente.Nome))
        {
            await Shell.Current.DisplayAlert("Erro", "Nome é obrigatório", "OK");
            return false;
        }

        await _service.SavePacienteAsync(Paciente);
        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
