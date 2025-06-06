using System.ComponentModel.DataAnnotations;
namespace api_aquaguard_dotnet.Models;

public class Regiao
{
    [Key]
    public int IdRegiao { get; set; }

    [Required]
    public string NmRegiao { get; set; }

    [Required]
    public string NmCidade { get; set; }

    public string CoordenadasLat { get; set; }
    public string CoordenadasLng { get; set; }

    public int IdSensor { get; set; }
    public Sensor Sensor { get; set; }

    public ICollection<Alerta> Alertas { get; set; }
}
