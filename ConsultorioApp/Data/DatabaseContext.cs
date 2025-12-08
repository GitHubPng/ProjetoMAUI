using SQLite;
using ConsultorioApp.Models;

namespace ConsultorioApp.Data;

public class DatabaseContext
{
    private SQLiteAsyncConnection? _database;

    public DatabaseContext()
    {
    }

    private async Task Init()
    {
        if (_database != null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "consultorio.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        
        await _database.CreateTableAsync<Paciente>();
        await _database.CreateTableAsync<Consulta>();
    }

    // ===== PACIENTES =====
    public async Task<List<Paciente>> GetPacientesAsync()
    {
        await Init();
        return await _database!.Table<Paciente>().ToListAsync();
    }

    public async Task<Paciente?> GetPacienteAsync(int id)
    {
        await Init();
        return await _database!.Table<Paciente>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SavePacienteAsync(Paciente paciente)
    {
        await Init();
        
        if (paciente.Id != 0)
            return await _database!.UpdateAsync(paciente);
        else
            return await _database!.InsertAsync(paciente);
    }

    public async Task<int> DeletePacienteAsync(Paciente paciente)
    {
        await Init();
        return await _database!.DeleteAsync(paciente);
    }

    // ===== CONSULTAS =====
    public async Task<List<Consulta>> GetConsultasAsync()
    {
        await Init();
        return await _database!.Table<Consulta>().ToListAsync();
    }

    public async Task<List<Consulta>> GetConsultasByDateAsync(DateTime date)
    {
        await Init();
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);
        
        return await _database!.Table<Consulta>()
            .Where(c => c.Data >= startDate && c.Data < endDate)
            .OrderBy(c => c.Horario)
            .ToListAsync();
    }

    public async Task<Consulta?> GetConsultaAsync(int id)
    {
        await Init();
        return await _database!.Table<Consulta>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveConsultaAsync(Consulta consulta)
    {
        await Init();
        
        if (consulta.Id != 0)
            return await _database!.UpdateAsync(consulta);
        else
            return await _database!.InsertAsync(consulta);
    }

    public async Task<int> DeleteConsultaAsync(Consulta consulta)
    {
        await Init();
        return await _database!.DeleteAsync(consulta);
    }
}
