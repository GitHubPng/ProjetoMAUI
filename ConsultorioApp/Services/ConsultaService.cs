using ConsultorioApp.Models;
using ConsultorioApp.Data;

namespace ConsultorioApp.Services;

public class ConsultaService
{
    private readonly DatabaseContext _database;

    public ConsultaService(DatabaseContext database)
    {
        _database = database;
    }

    public Task<List<Consulta>> GetConsultasAsync()
    {
        return _database.GetConsultasAsync();
    }

    public Task<List<Consulta>> GetConsultasByDateAsync(DateTime date)
    {
        return _database.GetConsultasByDateAsync(date);
    }

    public Task<Consulta?> GetConsultaAsync(int id)
    {
        return _database.GetConsultaAsync(id);
    }

    public Task<int> SaveConsultaAsync(Consulta consulta)
    {
        return _database.SaveConsultaAsync(consulta);
    }

    public Task<int> DeleteConsultaAsync(Consulta consulta)
    {
        return _database.DeleteConsultaAsync(consulta);
    }
}
