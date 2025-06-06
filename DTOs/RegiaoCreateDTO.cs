namespace api_aquaguard_dotnet.DTOs
{
    public class RegiaoCreateDTO
    {
        public string NmRegiao { get; set; }
        public string NmCidade { get; set; }
        public string CoordenadasLat { get; set; }
        public string CoordenadasLng { get; set; }
        public int IdSensor { get; set; }

    }
}
