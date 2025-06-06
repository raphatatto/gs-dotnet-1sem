using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_aquaguard_dotnet.Models
{
 
    [Table("TB_AQUA_REGIAO")]
    public class Regiao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_REGIAO")]      
        public int IdRegiao { get; set; }

        [Required]
        [Column("NM_REGIAO")]
        public string NmRegiao { get; set; }

        [Required]
        [Column("NM_CIDADE")]
        public string NmCidade { get; set; }

        [Column("COORDENADAS_LAT")]
        public string CoordenadasLat { get; set; }

        [Column("COORDENADAS_LNG")]
        public string CoordenadasLng { get; set; }

        [Column("ID_SENSOR")]
        public int IdSensor { get; set; }

        public Sensor Sensor { get; set; }

        public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
    }
}
