using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student = StudentAdminPortal.API.DomainModels.Student;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private object request;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {

            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }


        //Get All Students
        [HttpGet]
        [Route("[controller]/students")]

        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            // var domainModelStudents = new List<Student>();

            //foreach (var student in students)
            //{
            //    domainModelStudents.Add(new Student()
            //    {
            //        Id = student.Id,
            //        FirstName=student.FirstName,
            //        LastName=student.LastName,
            //        DateOfBirth=student.DateOfBirth,
            //        Email=student.Email,
            //        Mobile=student.Mobile,
            //        ProfileImageUrl=student.ProfileImageUrl,
            //        GenderId=student.GenderId,

            //        Address = new Address() {
            //            Id = student.Address.Id,
            //            PhysicalAddress=student.Address.PhysicalAddress,
            //            PostalAddress=student.Address.PostalAddress,
            //        },

            //        Gender = new Gender()
            //        {
            //            Id = student.GenderId,
            //            Description= student.Gender.Description
            //        }


            //    });          

            //}



            return Ok(mapper.Map<List<Student>>(students));
        }

        //Get Single Student  by ID
        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch Single Student Details
            var student = await studentRepository.GetStudentAsync(studentId);

            //return student
            if (student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }



        //Update Student in Table
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest updateStudentrequest)
        {
            if (await studentRepository.Exists(studentId))
            {
                //var StudentDataModel
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(updateStudentrequest));

                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }

            return NotFound();

        }
    }
}