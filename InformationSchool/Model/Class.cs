using InformationSchool.Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string  TeacherId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

       // public virtual Teacher Teacher { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }


        public Class(string Name, string TeacherId, DateTime UpdatedDate, string UpdatedBy)
        {
            this.Name = Name;
            this.TeacherId = TeacherId;
            this.UpdatedBy = UpdatedBy;
            this.UpdatedDate = UpdatedDate;
        }
    }
}
