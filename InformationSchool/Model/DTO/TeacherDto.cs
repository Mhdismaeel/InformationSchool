using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.DTO
{
    public class TeacherDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ImagePath { get; set; }

        public string RoleName { get; set; }

        public List<string> TeacherClasses { get; set; }
    }
}
