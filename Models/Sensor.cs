using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_aquaguard_dotnet.Models
{

    [Table("TB_AQUA_SENSOR")]
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_SENSOR")]
        public int IdSensor { get; set; }

        [Required]
        [Column("TIPO")]
        public string Tipo { get; set; }

        [Required]
        [RegularExpression("ATIVO|INATIVO")]
        [Column("STATUS")]
        public string Status { get; set; }

        public ICollection<Regiao> Regioes { get; set; } = new List<Regiao>();
    }
}
