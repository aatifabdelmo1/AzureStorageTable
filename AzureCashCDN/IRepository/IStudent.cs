using AzureCashCDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.IRepository
{
  public  interface IStudent
    {
        Task<IEnumerable<Student>> GetAllStudents();

        Task<Student> GetStudentById(int StudentId);

        Task<bool> CreateStudent(Student Student);

        Task<bool> UpdateStudent(int id,Student Student);

        Task<bool> DeleteStudent(Student Student);

        Task<bool> Save();
    }
}
