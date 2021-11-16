using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.Auth
{
    public class AdminRegisterModel
    {


        public string Id { get; set; }
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public  string Address { get; set; }

        public string ImageProfile { get; set; }
        public  string RoleName { get; set; }


       
    }
}
