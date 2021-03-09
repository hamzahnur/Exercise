using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("Tb_M_University")]
    public class University
    {
        [Key]
        [Required]
        public int UniversityID { get; set; }
        [Required]
        public string UniversityName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}
