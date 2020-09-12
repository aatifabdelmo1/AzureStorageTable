using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [Required]
        public string StudentName { get; set; }

        
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        
    }
}
