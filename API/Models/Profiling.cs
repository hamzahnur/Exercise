using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("Tb_T_Profiling")]
    public class Profiling
    {
        [Key]
        [Required]
        public string NIK { get; set; }
        [Required]
        public string EducationID { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
