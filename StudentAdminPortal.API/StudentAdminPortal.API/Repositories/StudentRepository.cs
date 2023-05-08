using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public Task<bool> Exists(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gender>> GetGendersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentAsync(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            throw new NotImplementedException();
        }
    }
}
