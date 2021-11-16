using InformationSchool.Context;
using InformationSchool.Model;
using InformationSchool.Model.Auth;
using InformationSchool.Model.Binding;
using InformationSchool.Model.DTO;
using InformationSchool.Repository.Interfaces.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Repository.Services.Admin
{
    public class AdminServices: IAdminServices
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //-----------------teachers services
       public  List<TeacherDto> GetAllTeacher()
        {
            var Role = db.Roles.FirstOrDefault(x=>x.Name=="Teacher");
            var TeacherIds = db.UserRoles.Where(x => x.RoleId == Role.Id).Select(y=>y.UserId).Distinct();
            var Teacher = db.Users.Where(x => TeacherIds.Contains(x.Id)).Include(y => y.Classes);
            
            var response = Teacher.Select(x => new TeacherDto()
            {
              
                Id = x.Id,
                Name = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                ImagePath = x.ImageProfile,
                RoleName = "Teacher",
                TeacherClasses=x.Classes.Select(c=>c.Name).ToList()
            }).ToList();

            return response;
        }

        public bool DeleteTeacher(string id)
        {
            var teacher = db.Users.FirstOrDefault(x => x.Id == id);
            teacher.IsDeleted = true;
            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public async Task<TeacherDto> UpdateTeacher(AdminRegisterModel model)
        {
            var UserInfo = await _userManager.FindByIdAsync(model.Id);
            var userRole = await _userManager.GetRolesAsync(UserInfo);
            var OldUser = db.Users.FirstOrDefault(x=>x.Id==model.Id);

            if (UserInfo == null)
            {
                return null;
            }

          
            if (userRole!=null)
            {
                await _userManager.RemoveFromRolesAsync(UserInfo, userRole.ToArray());

            }

            UserInfo.FirstName = model.FirstName;
            UserInfo.LastName = model.LastName;
            UserInfo.UserName = model.Username;
            UserInfo.Email = model.Email;
            UserInfo.PhoneNumber = model.PhoneNumber;
            UserInfo.Adress = model.Address;
            UserInfo.ImageProfile = model.ImageProfile;

            await _userManager.UpdateAsync(UserInfo);
         
            await _userManager.AddToRoleAsync(UserInfo, model.RoleName);
            db.SaveChanges();

            var response=  new TeacherDto()
            {
                Id = model.Id,
                Name = model.FirstName + "_" + model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ImagePath = model.ImageProfile,
                RoleName = model.RoleName
            };

            return response;
        }

       //---------------------------------student service

        public List<StudentDto> GetAllStudent()
        {
            var Students = db.Users.Include(x=>x.ClassStudents).ThenInclude(v=>v.Class).ThenInclude(y=>y.ApplicationUser);

            var StudentRole = db.Roles.FirstOrDefault(x=>x.Name=="Student");
            var StudentIds = db.UserRoles.Where(x=>x.RoleId== StudentRole.Id).Select(y=>y.UserId).Distinct();
            var response = Students.Select(x => new StudentDto()
            {
                StudentId = x.Id,
                StudentName = x.UserName,
                StudentEmail = x.Email,
                StudentPhoneNumber = x.PhoneNumber,
                StudentImagePath = x.ImageProfile,
                StudentClasses = x.ClassStudents.Select(z=>new StudentClassDto() { 
                    ClassId=z.ClassId,
                    ClassName=z.Class.Name,
                    TeacherName=z.Class.ApplicationUser.UserName,
                    TeacherId=z.Class.ApplicationUser.Id
                }).ToList()
            });

            

            return response.Where(x=> StudentIds.Contains(x.StudentId)).ToList();
        }


       public bool DeleteStudent(string id)
        {
            var student = db.Users.FirstOrDefault(x => x.Id == id);

            student.IsDeleted = true;

            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public StudentDto UpdateStudent(StudentBinding model)
        {
            var student = db.Users.FirstOrDefault(x=>x.Id==model.StudentId);

            student.FirstName = model.FName;
            student.LastName = model.LName;
            student.UserName = model.UserName;
            student.Email = model.StudentEmail;
            student.PhoneNumber = model.StudentPhone;
            student.Adress = model.Address;
            student.ImageProfile = model.ImageProfile;
            student.UpdatedDate = DateTime.Now;

            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return new StudentDto()
            {
                StudentId = model.StudentId,
                StudentName = model.UserName,
                StudentEmail = model.StudentEmail,
                StudentPhoneNumber = model.StudentPhone,
                StudentImagePath = model.ImageProfile
            };

          
        }

        public List<CourseDto> GetAllCourse()
        {
            var courses = db.courses.Include(x => x.CourseType).Include(y => y.Class);
            var CourseList = courses.Select(x => new CourseDto() { 
                Id=x.Id,
                Name=x.Name,
                CoursePath=x.CoursePath,
                CourseType=x.CourseType.Name,
                ClassName=x.Class.Name
            }).ToList();

            return CourseList;
        }

        public bool DeleteCourse(int id)
        {
            var course = db.courses.FirstOrDefault(x=>x.Id==id);

            course.IsDeleted = true;

            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return true;

        }

        public Course UpdateCourse(Course model)
        {
            var course = db.courses.FirstOrDefault(x=>x.Id==model.Id);

           var Ecourse= new Course(model.Name,model.CoursePath,model.CourseTypeId,model.ClassId,DateTime.Now);

            db.Entry(Ecourse).State = EntityState.Modified;
            db.SaveChanges();
            return model;


        }

        public Course CreateCource(Course model)
        {
            db.courses.Add(model);
            db.SaveChanges();
            return model;
        }

        //----------------------------------class service

        public List<ClassDto> GetAllClass()
        {
            var classes = db.classes.Include(x=>x.Courses).Include(y=>y.ApplicationUser);

            var ClassList = classes.Select(x => new ClassDto() {
                Id = x.Id,
                Name = x.Name,
                TeacherName = x.ApplicationUser.UserName,
                ClassCourses = x.Courses.Select(z => new CourseClassDto() {
                    CourseId=z.Id,
                    CourseName=z.Name
                }).ToList()
            }).ToList();

            return ClassList;
        }

        public bool DeleteClass(int id)
        {
            var Dclass = db.classes.FirstOrDefault(x=>x.Id==id);

            Dclass.IsDeleted=true;

            db.Entry(Dclass).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public Class CreateClass(Class model)
        {
            model.CreatedDate = DateTime.Now;
            db.classes.Add(model);
            db.SaveChanges();
            return model;
        }


        public Class UpdateClass(Class model)
        {
            var OldClass = db.classes.FirstOrDefault(x=>x.Id==model.Id);

            var Eclass = new Class(model.Name,model.TeacherId,DateTime.Now,"Test");
            db.Entry(Eclass).State = EntityState.Modified;
            db.SaveChanges();
            return model;
        }
    }
}
