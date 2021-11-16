using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.Auth
{

    [NotMapped]
    public class ApplicationUser: IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public string Adress { get; set; }

        public string? ImageProfile { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Class> Classes { get; set; }  //as teacher

        public virtual ICollection<ClassStudent> ClassStudents { get; set; } // as students



}
}
