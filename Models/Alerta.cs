using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_aquaguard_dotnet.Models
{
    [Table("TB_AQUA_ALERTA")]
    public class Alerta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_ALERTA")]
        public int IdAlerta { get; set; }

        [Required]
        [RegularExpression("BAIXO|MEDIO|ALTO")]
        [Column("NIVEL_RISCO")]
        public string NivelRisco { get; set; }

        [Column("DS_ALERTA")]
        public string DsAlerta { get; set; }

        [Column("DT_ALERTA")]
        public DateTime? DtAlerta { get; set; }

        [Column("ID_REGIAO")]
        public int IdRegiao { get; set; }
        public Regiao Regiao { get; set; }
    }
}
