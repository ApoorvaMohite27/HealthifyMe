using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthifyMeFinalProject.Models
{
    [Table("FeedBackForms")]
    public class FeedBackForm
    {
        [Key]                                                      // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]      // Identity (1,1)
        public int FormId { get; set; }

        // Customer Name
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        public string Rating { get; set; }

        [Required]
        [StringLength(50)]
        public string Comments { get; set; }
    }
}
/*********************************************
        CREATE TABLE [FeedBackForms]
        (
	        [FormId] int NOT NULL IDENTITY(1,1)
	        , [CustomerName] varchar(60)
            , [Rating] varchar(60) NOT NULL
            , [Comments] varchar(60) NOT NULL
	        , CONSTRAINT [PK_FeedBackForm] PRIMARY KEY ( [FormId] ASC )
        )
        GO
   **********/
