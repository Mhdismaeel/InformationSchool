using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.DTO
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public List<CourseClassDto> ClassCourses { get; set; }

       
    }

    public class CourseClassDto
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }
    }
}
