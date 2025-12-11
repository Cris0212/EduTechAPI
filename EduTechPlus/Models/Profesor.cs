using System.ComponentModel.DataAnnotations.Schema;

public class Profesor
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Colegio { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public int GruposQueDa { get; set; }
    public object Usuario { get; internal set; }
}