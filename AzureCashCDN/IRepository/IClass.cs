using AzureCashCDN.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.IRepository
{
    public interface IClass
    {
       Task<IEnumerable<Class>> GetAllClasses();

        Task<Class> GetClassById(int ClassId);

        Task<bool> CreateClass(Class Cl);

        Task<bool> UpdateClass(Class Cl);

        Task<bool> DeleteClass(Class Cl);

        Task<bool> Save();
    }
}
