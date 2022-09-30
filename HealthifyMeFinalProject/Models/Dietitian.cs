using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthifyMeFinalProject.Models
{
    [Table("Dietitians")]
    public class Dietitian
    {
        [Key]                                                    // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    // Identity (1,1)
        [Display(Name = "Dietitian Id")]
        public int DietitianId { get; set; }

        // User Name of Dietitian
        [Required]
        [StringLength(450)]
        [Display(Name = "Dietitian User Name")]
        public string DietitianUserName { get; set; }

        [Required]
        [Display(Name = "Dietitian Name")]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "{0} only contains characters!")]
        public string DietitianName { get; set; }

        [Required]
        public string Specialist { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Dietitian Photo")]
        public string DietitianPhoto { get; set; }


        #region Navigation Properties to AssignTask Model

        public ICollection<AssignTask> AssignTasks { get; set; }

        #endregion
    }
}
