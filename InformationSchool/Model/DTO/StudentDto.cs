using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.DTO
{
    public class StudentDto
    {
        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public string StudentEmail { get; set; }

        public string StudentPhoneNumber { get; set; }

        public string StudentImagePath { get; set; }

        public List<StudentClassDto> StudentClasses { get; set; }

    }

    public class StudentClassDto
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public string TeacherName { get; set; }

        public string TeacherId { get; set; }
    }
    
}
