using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.DTO
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoursePath { get; set; }

        public string CourseType { get; set; }

        public string ClassName { get; set; }

    }
}
