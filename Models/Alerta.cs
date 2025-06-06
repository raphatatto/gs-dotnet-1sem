using System.ComponentModel.DataAnnotations;

namespace api_aquaguard_dotnet.Models
{
    public class Alerta
    {
        [Key] 
        public int IdAlerta { get; set; }

        [Required]
        [RegularExpression("BAIXO|MEDIO|ALTO")]
        public string NivelRisco { get; set; }

        public string DsAlerta { get; set; }
        public DateTime? DtAlerta { get; set; }

        public int IdRegiao { get; set; }
        public Regiao Regiao { get; set; }
    }
}
