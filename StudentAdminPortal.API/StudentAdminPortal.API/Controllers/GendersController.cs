using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    public class GendersController : Controller
    {
        private readonly StudentRepository studentRepository;

        public GendersController(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }



        //[HttpGet]
        //[Route("[controller]/genders")]
        //public IActionResult GetAllGenders()
        //{
        //    var genderList = studentRepository.GetGendersAsync();
        //    if (genderList == null | !genderList.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(genderList);
        //}
        [HttpGet]
        [Route("[controller]/genders")]
        public IActionResult GetAllGenders()
        {
            var genderList = studentRepository.GetGendersAsync();
            if(genderList == null | !genderList.Any()){

                return NotFound();
            } 
            return Ok(genderList);
        }
    }
}
