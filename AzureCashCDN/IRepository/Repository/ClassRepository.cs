using AzureCashCDN.Data;
using AzureCashCDN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AzureCashCDN.IRepository.Repository
{
    public class ClassRepository : IClass
    {
        private readonly SchoolContext _db;
        //private readonly IDistributedCache _Cache;

        public ClassRepository(SchoolContext db)
        {
            _db = db;
            //_Cache = Cache;
        }

        public async Task<bool> CreateClass(Class Cl)
        {
            _db.Add(Cl);
            return await Save();
        }

        public async Task<bool> DeleteClass(Class Cl)
        {

            _db.Remove(Cl);

            return await Save();


        }

        public async Task<IEnumerable<Class>> GetAllClasses()
        {
            //List<Class> Classes = new List<Class>();
            //string cacheClass = _Cache.GetString("Classes");
            //if (!string.IsNullOrEmpty(cacheClass))
            //{
            //    Classes = JsonConvert.DeserializeObject<List<Class>>(cacheClass);
            //}
            //else
            //{
                var Classes = await _db.Class.Include(S=>S.students).ToListAsync();
            //    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            //    options.SetAbsoluteExpiration(new System.TimeSpan(0,15,0));
            //    _Cache.SetString("Classes", JsonConvert.SerializeObject(Classes, Formatting.None, new JsonSerializerSettings() {ReferenceLoopHandling=ReferenceLoopHandling.Ignore }), options) ;
            //}


            return Classes;
        }

        public async Task<Class> GetClassById(int ClassId)
        {
            var Class = await _db.Class.FindAsync(ClassId);
            return Class;
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UpdateClass(Class Cl)
        {
            _db.Class.Update(Cl);
            return await Save();

        }
    }
}
