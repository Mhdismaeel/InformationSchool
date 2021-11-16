using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CoursePath { get; set; }



        [ForeignKey("CourseType")]
        public int CourseTypeId { get; set; }

        [ForeignKey("Class")]

        public int ClassId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Class Class { get; set; }
        public virtual CourseType CourseType { get; set; }


        public Course( string Name, string CoursePath, int CourseTypeId, int ClassId, DateTime UpdatedDate)
        {
            this.Name = Name;
            this.CoursePath = CoursePath;
            this.CourseTypeId = CourseTypeId;
            this.ClassId = ClassId;
            this.UpdatedDate = UpdatedDate;
        }
    }
}
