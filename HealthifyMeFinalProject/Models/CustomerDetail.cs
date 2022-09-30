using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthifyMeFinalProject.Models
{
    [Table("CustomerDetails")]
    public class CustomerDetail
    {
        [Key]                                                     // Primary Key   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]     // Identity (1,1)
        public int CustomerId { get; set; }

        [StringLength(450)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "{0} only contains characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "{0} only contains characters!")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Range(10, 60, ErrorMessage = "{0} should be greater than 10 years!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Current Weight")]
        public decimal CurrentWeight { get; set; }

        [Display(Name = "Target Weight")]
        [Required(ErrorMessage = "{0} cannot be empty!")]
        public decimal TargetWeight { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        public string MedicalCondition { get; set; }



        #region Navigation Properties to AssignTask Model

        public ICollection<AssignTask> AssignTasks { get; set; }

        #endregion
    }
}
