namespace api_aquaguard_dotnet.DTOs
{
    public class AlertaDTO
    {
        public int IdAlerta { get; set; }
        public string NivelRisco { get; set; }
        public string DsAlerta { get; set; }
        public DateTime? DtAlerta { get; set; }
        public int IdRegiao { get; set; }
 
    }
}
