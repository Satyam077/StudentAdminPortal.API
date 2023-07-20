using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentRepository studentRepository;
        private readonly ImageRepository imageRepository;
        private object request;

        public StudentController(StudentRepository studentRepository, ImageRepository imageRepository)
        {

            this.studentRepository = studentRepository;
            this.imageRepository = imageRepository;
        }



        //Get All Students
        [HttpGet("[controller]/students")]
        public IActionResult GetAllStudentsAsync()
        {
            var students = studentRepository.GetStudentsAsync();

            return Ok(students);
        }

        //Get Single Student  by ID
        [HttpGet("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public IActionResult GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch Single Student Details
            var student = studentRepository.GetStudentAsync(studentId);

            //return student
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        //Update Student in Table
        [HttpPut("[controller]/{studentId:guid}")]
        public IActionResult UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] Student updateStudentrequest)
        {
            if (studentRepository.Exists(studentId))
            {
                //var StudentDataModel
                var updatedStudent = studentRepository.UpdateStudent(studentId, updateStudentrequest);

                if (updatedStudent != null)
                {
                    return Ok(updatedStudent);
                }
            }

            return NotFound();

        }

        [HttpDelete("[controller]/{studentId:guid}")]
        public IActionResult DeleteStudent([FromRoute] Guid studentId)
        {
            //check student ID
            if (studentRepository.Exists(studentId))
            {
                var student = studentRepository.DeleteStudent(studentId);
                return Ok(student);

            }
            return NotFound();
        }

        [HttpPost("[controller]/Add")]
        public IActionResult AddStudentAsync([FromBody] Student request)
        {
            request.Id = Guid.NewGuid();

            var student = studentRepository.AddStudent(request);

            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, student);
        }

        [HttpPost("[controller]/{studentId:guid}/upload-image")]
        public IActionResult UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            //checks if students exists
            if (studentRepository.Exists(studentId))
            {
                //upload the image from local storage
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                //update the Profile image in the database
                var fileImagePath = imageRepository.Upload(profileImage, fileName);

                if (studentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Uploading Image");
            }
            return NotFound();
        }
    }
}