using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthifyMeFinalProject.Models
{
    [Table("AssignTasks")]
    public class AssignTask
    {
        [Key]                                                      // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]     // Identity (1,1)
        public int AssignTaskId { get; set; }

        
        [StringLength(1000)]
        public string Diet { get; set; }

        
        [StringLength(1000)]
        public string Exercise { get; set; }


        #region Navigation to CustomerDetail Model

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(AssignTask.CustomerId))]
        public CustomerDetail CustomerDetail { get; set; }

        #endregion


        #region Navigation to Dietitian Model

        [Required]
        public int DietitianId { get; set; }

        [ForeignKey(nameof(AssignTask.DietitianId))]
        public Dietitian Dietitian { get; set; }

        #endregion

    }
}
