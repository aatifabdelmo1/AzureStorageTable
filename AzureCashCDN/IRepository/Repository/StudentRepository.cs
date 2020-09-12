using AzureCashCDN.Data;
using AzureCashCDN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.IRepository.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly SchoolContext _db;

        public StudentRepository(SchoolContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateStudent(Student Student)
        {
            _db.Student.Add(Student);
            return await Save();
        }

        public async Task<bool> DeleteStudent(Student Student)
        {
            _db.Student.Remove(Student);
            return await Save();
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await _db.Student.Include(c=>c.Class).ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentById(int StudentId)
        {
            var student = await _db.Student.Include(c => c.Class).Where(S=>S.Id==StudentId).SingleOrDefaultAsync();
            return student;
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UpdateStudent(int id,Student Student)
        {
            var student = await _db.Student.FindAsync(id);
            student.StudentName = Student.StudentName;
            Student.ClassId = Student.ClassId;
            return await Save();
        }
    }
}
