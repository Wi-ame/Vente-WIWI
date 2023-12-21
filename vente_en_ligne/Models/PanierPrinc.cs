using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vente_en_ligne.Models
{
    public class PanierPrinc
    {
        [Key]
        public int PID { get; set; }

        [ForeignKey("Utilisateur")]
        [Required]
        public  int IDU { get; set; } 
        public Utilisateur Utilisateur { get; set; }
        [Required]
        public DateTime DateCréation { get; set; }  
    }
}
