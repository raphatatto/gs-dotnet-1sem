namespace api_aquaguard_dotnet.DTOs
{
    public class RegiaoDTO
    {
        public int? IdRegiao { get; set; }
        public string NmRegiao { get; set; }
        public string NmCidade { get; set; }
        public string CoordenadasLat { get; set; }
        public string CoordenadasLng { get; set; }
        public int IdSensor { get; set; }
    }
}
