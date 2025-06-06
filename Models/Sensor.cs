using System.ComponentModel.DataAnnotations;
namespace api_aquaguard_dotnet.Models;
public class Sensor
{
    [Key]
    public int IdSensor { get; set; }
    public string Tipo { get; set; }

    [RegularExpression("ATIVO|INATIVO")]
    public string Status { get; set; }

    public ICollection<Regiao> Regioes { get; set; } = new List<Regiao>();

}
