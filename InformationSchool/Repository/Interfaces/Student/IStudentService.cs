using InformationSchool.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Repository.Interfaces.Student
{
    public  interface IStudentService
    {
        List<TeacherDto> GetStudentClass();

        ClassDto GetClassDetails(int id);
    }
}
