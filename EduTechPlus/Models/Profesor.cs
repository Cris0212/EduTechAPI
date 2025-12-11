using System.ComponentModel.DataAnnotations.Schema;

public class profesor
{
    public int id { get; set; }
    public int usuarioid { get; set; }
    public string colegio { get; set; } = string.Empty;
    public string turno { get; set; } = string.Empty;
    public int gruposqueda { get; set; }
}