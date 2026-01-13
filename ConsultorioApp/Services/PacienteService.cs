using ConsultorioApp.Models;
using ConsultorioApp.Data;

namespace ConsultorioApp.Services;

public class PacienteService
{
    private readonly DatabaseContext _database;

    public PacienteService(DatabaseContext database)
    {
        _database = database;
    }

    public Task<List<Paciente>> GetPacientesAsync()
    {
        return _database.GetPacientesAsync();
    }

    public Task<Paciente?> GetPacienteAsync(int id)
    {
        return _database.GetPacienteAsync(id);
    }

    public Task<int> SavePacienteAsync(Paciente paciente)
    {
        return _database.SavePacienteAsync(paciente);
    }

    public Task<int> DeletePacienteAsync(Paciente paciente)
    {
        return _database.DeletePacienteAsync(paciente);
    }
}
