namespace ConsultorioApp.Models;

public class Consulta
{
    public int Id { get; set; }
    public string Paciente { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Horario { get; set; }
    public string Observacoes { get; set; }
}
