using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCashCDN.IRepository;
using AzureCashCDN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzureCashCDN.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _SR;
        private readonly IClass _CR;

        public StudentController(IStudent SR,IClass CR)
        {
            _SR = SR;
            _CR = CR;
        }

        public IActionResult Index()
        {
            var Students = _SR.GetAllStudents().GetAwaiter().GetResult();
            return View(Students);
        }

        public IActionResult Details(int id)
        {
            var student = _SR.GetStudentById(id).GetAwaiter().GetResult();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ClassStudentVM Vm = new ClassStudentVM();
            Vm.Student = new Models.Student();
            Vm.Classes = _CR.GetAllClasses().GetAwaiter().GetResult().ToList();
            List<SelectListItem> SL = new List<SelectListItem>();
            foreach(var Class in Vm.Classes)
            {
                SelectListItem Item = new SelectListItem()
                {
                    Text = Class.ClassName,
                    Value = Class.Id.ToString()
                    
                };
            SL.Add(Item);
            }

            ViewBag.List = SL;

            return View(Vm);
        }

        [HttpPost]
        public IActionResult Create(ClassStudentVM model)
        {
            if (ModelState.IsValid)
            {
               if (_SR.CreateStudent(model.Student).GetAwaiter().GetResult())
                {
                    return RedirectToAction("Index");
                }
            }

            ClassStudentVM model1 = new ClassStudentVM();
            model1.Classes= _CR.GetAllClasses().GetAwaiter().GetResult().ToList();
            model1.Student = model.Student;
            List<SelectListItem> SL = new List<SelectListItem>();
            foreach (var Class in model1.Classes)
            {
                SelectListItem Item = new SelectListItem()
                {
                    Text = Class.ClassName,
                    Value = Class.Id.ToString()

                };
                SL.Add(Item);
            }

            ViewBag.List = SL;

            return View(model1);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Student = _SR.GetStudentById(id).GetAwaiter().GetResult();
            ClassStudentVM Vm = new ClassStudentVM();
            Vm.Student = Student;
            Vm.Classes = _CR.GetAllClasses().GetAwaiter().GetResult().ToList();
            List<SelectListItem> SL = new List<SelectListItem>();
            foreach (var Class in Vm.Classes)
            {
                SelectListItem Item = new SelectListItem()
                {
                    Text = Class.ClassName,
                    Value = Class.Id.ToString()

                };
                SL.Add(Item);
            }

            ViewBag.List = SL;

            return View(Vm);
        }

        [HttpPost]
        public IActionResult Edit(int id,ClassStudentVM model)
        {
            if (ModelState.IsValid)
            {
                if (_SR.UpdateStudent(id,model.Student).GetAwaiter().GetResult())
                {
                    return RedirectToAction("Index");
                }
            }

            ClassStudentVM model1 = new ClassStudentVM();
            model1.Classes = _CR.GetAllClasses().GetAwaiter().GetResult().ToList();
            model1.Student = model.Student;
            List<SelectListItem> SL = new List<SelectListItem>();
            foreach (var Class in model1.Classes)
            {
                SelectListItem Item = new SelectListItem()
                {
                    Text = Class.ClassName,
                    Value = Class.Id.ToString()

                };
                SL.Add(Item);
            }

            ViewBag.List = SL;

            return View(model1);
        }

    }


}
