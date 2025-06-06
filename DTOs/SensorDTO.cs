namespace api_aquaguard_dotnet.DTOs
{
    public class SensorDTO
    {
        public int? IdSensor { get; set; }  // usado no PUT
        public string Tipo { get; set; }
        public string Status { get; set; }
    }
}
