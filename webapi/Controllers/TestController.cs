using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Model;

namespace webapi.Controllers
{
    


    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private static List<Student> first = new List<Student>()
        {
            new Student(){Id=1,Name="S1"},
            new Student(){Id=2,Name="S2"},
            new Student(){Id=3,Name="S3"},
        };

        [HttpGet]
        public IActionResult Get()
        {
            var students = first.ToList();
            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "Getbyid")]
        public IActionResult Getbyid(int id)
        {
            var student = first.Where(s => s.Id == id).SingleOrDefault();
            return Ok(student);
        }

    }
}


