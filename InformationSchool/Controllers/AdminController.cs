using InformationSchool.Model;
using InformationSchool.Model.Auth;
using InformationSchool.Model.Binding;
using InformationSchool.Repository.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _IAdminServices;

        public AdminController(IAdminServices adminServices)
        {
            _IAdminServices = adminServices;
        }

        [HttpGet("GetTeachers")]

        public IActionResult GetTeachers()
        {
            return Ok(_IAdminServices.GetAllTeacher());
        }

        [HttpPost("UpdateTeacher")]

        public async Task<IActionResult>  UpdateTeacher(AdminRegisterModel model)
        {
            var result = await _IAdminServices.UpdateTeacher(model);
            return Ok(result);
        }

        [HttpDelete("RemoveTeacher")]

        public IActionResult RemoveTeacher(string id)
        {
            return Ok(_IAdminServices.DeleteTeacher(id));
        }

        //-------------------------------------------------student controller

        [HttpGet("GetAllStudent")]

        public IActionResult GetAllStudent()
        {
            return Ok(_IAdminServices.GetAllStudent());
        }

        [HttpPost("EditStudent")]

        public IActionResult EditStudent(StudentBinding model)
        {
            return Ok(_IAdminServices.UpdateStudent(model));
        }

        //---------------------------------------------------course controller

        [HttpGet("GetAllCourse")]

        public IActionResult GetAllCourse()
        {
            return Ok(_IAdminServices.GetAllCourse());
        }

        [HttpGet("RemoveCourse")]

        public IActionResult RemoveCourse(int id)
        {
            return Ok(_IAdminServices.DeleteCourse(id));
        }

        [HttpPost("EditCourse")]

        public IActionResult EditCourse(Course model)
        {
            return Ok(_IAdminServices.UpdateCourse(model));
        }

        [HttpPost("CreateCourse")]

        public IActionResult CreateCourse(Course model)
        {
            return Ok(_IAdminServices.CreateCource(model));
        }

        //------------------------------------------class conroller

        [HttpGet("GetAllClass")]

        public IActionResult GetAllClass()
        {
            return Ok(_IAdminServices.GetAllClass());
        }

        [HttpDelete("DeleteClass")]

        public IActionResult DeleteClass(int id)
        {
            return Ok(_IAdminServices.DeleteClass(id));
        }


        [HttpPost("CreateClass")]

        public IActionResult CreateClass(Class model)
        {
            return Ok(_IAdminServices.CreateClass(model));
        }

        [HttpPost("EditClass")]

        public IActionResult EditClass(Class model)
        {
            return Ok(_IAdminServices.UpdateClass(model));
        }
    }
}
