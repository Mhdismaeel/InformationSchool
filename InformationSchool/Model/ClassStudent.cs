using InformationSchool.Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model
{
    public class ClassStudent
    {
    
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string StudentId { get; set; }

       [ForeignKey("Class")]
       [Required]
        public int ClassId { get; set; }

        public double ? FirstTerm { get; set; }
        public double? MidtTerm { get; set; }
        public double? FinaltTerm { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

       // public virtual Student Student { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Class Class { get; set; }

    }
}
