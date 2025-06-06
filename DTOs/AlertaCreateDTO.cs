// DTO apenas para criação de um alerta
using System.ComponentModel.DataAnnotations;

namespace api_aquaguard_dotnet.DTOs
{
    public class AlertaCreateDTO
    {
        [Required]
        [RegularExpression("BAIXO|MEDIO|ALTO")]
        public string NivelRisco { get; set; }

        public string DsAlerta { get; set; }
        public DateTime? DtAlerta { get; set; }

        [Required]
        public int IdRegiao { get; set; }
    }
}
