using ConsultorioApp.Models;
using ConsultorioApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsultorioApp.ViewModels;

public class ConsultaFormViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly ConsultaService _service;
    
    private Consulta _consulta;
    public Consulta Consulta
    {
        get => _consulta;
        set
        {
            _consulta = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public ConsultaFormViewModel(ConsultaService service)
    {
        _service = service;
        Consulta = new Consulta 
        { 
            Data = DateTime.Today,
            Horario = new TimeSpan(9, 0, 0),
            Paciente = "",
            Observacoes = ""
        };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("ConsultaId"))
        {
            var id = int.Parse(query["ConsultaId"].ToString()!);
            _ = CarregarConsultaAsync(id);
        }
    }

    private async Task CarregarConsultaAsync(int id)
    {
        var consulta = await _service.GetConsultaAsync(id);
        if (consulta != null)
            Consulta = consulta;
    }

    public async Task<bool> SalvarAsync()
    {
        if (string.IsNullOrWhiteSpace(Consulta.Paciente))
        {
            await Shell.Current.DisplayAlert("Erro", "Nome do paciente é obrigatório", "OK");
            return false;
        }

        await _service.SaveConsultaAsync(Consulta);
        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
