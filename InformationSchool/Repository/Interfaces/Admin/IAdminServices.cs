using InformationSchool.Model;
using InformationSchool.Model.Auth;
using InformationSchool.Model.Binding;
using InformationSchool.Model.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Repository.Interfaces.Admin
{
   public interface IAdminServices
    {
        //-----------------teachers services
        List<TeacherDto> GetAllTeacher();
        bool DeleteTeacher(string id);
        Task<TeacherDto> UpdateTeacher(AdminRegisterModel model);

        //-------------------------Student Service
        List<StudentDto> GetAllStudent();
         bool DeleteStudent(string id);
         StudentDto UpdateStudent(StudentBinding model);
        //------------------------------------course
          List<CourseDto> GetAllCourse();
           bool DeleteCourse(int id);
          Course UpdateCourse(Course model);
          Course CreateCource(Course model);
                 
          //--------------------------------------Classes
          List<ClassDto> GetAllClass();
          bool DeleteClass(int id);
          Class UpdateClass(Class model);
          Class CreateClass(Class model);
        //----------------------------------------------------Role

        List<IdentityRole> GetRoles();
    }
}
