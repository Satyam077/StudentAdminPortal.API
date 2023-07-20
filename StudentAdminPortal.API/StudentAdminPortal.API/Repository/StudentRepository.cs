using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Address = StudentAdminPortal.API.DataModels.Address;
using Gender = StudentAdminPortal.API.DataModels.Gender;
using Student = StudentAdminPortal.API.DataModels.Student;

namespace StudentAdminPortal.API.Repositories
{
    public class StudentRepository
    {
        private readonly StudentAdminContext context;

        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }


        // Methods to call from Controller
        public List<Student> GetStudentsAsync()
        {
            return context.Student
            .Include(nameof(Gender))
            .Include(nameof(Address))
            .ToList();
        }

        public Student GetStudentAsync(Guid studentId)
        {
            return context.Student
                 .Include(nameof(Gender))
                 .Include(nameof(Address))
                 .FirstOrDefault(x => x.Id == studentId);
        }

        public List<Gender> GetGendersAsync()
        {
            return context.Gender.ToList();
        }

        public bool Exists(Guid studentId)
        {
            return context.Student.Any(x => x.Id == studentId);
        }

        public Student UpdateStudent(Guid studentId, Student request)
        {

            var existingStudent  = GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;

                existingStudent.Address = new Address();
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                context.SaveChanges();
                return existingStudent;

            }
            return null;
        }

        public Student DeleteStudent(Guid studentId)
        {

            var student = GetStudentAsync(studentId);
            if (student != null)
            {
                context.Student.Remove(student);
                context.SaveChanges();
                return student;
            }
            return null;
        }

        public Student AddStudent(Student request)
        {
            var student = context.Student.Add(request);
            context.SaveChanges();
            return student.Entity;



        }

        public bool UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = GetStudentAsync(studentId);

            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

