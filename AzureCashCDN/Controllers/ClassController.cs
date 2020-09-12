using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCashCDN.IRepository;
using AzureCashCDN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureCashCDN.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClass _CR;

        public ClassController(IClass CR)
        {
            _CR = CR;
        }
        public IActionResult Index()
        {
            var Classes=_CR.GetAllClasses().GetAwaiter().GetResult();
            return View(Classes);
        }

        public IActionResult Details(int id)
        {
            var Class =_CR.GetClassById(id).GetAwaiter().GetResult();
            return View(Class);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Class Cl)
        {
            if (ModelState.IsValid)
            {
                if (_CR.CreateClass(Cl).GetAwaiter().GetResult())
                {
                    return RedirectToAction("Index");
                }
            }
            return View(Cl);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Class = _CR.GetClassById(id).GetAwaiter().GetResult();

            if (Class !=null)
            {
                return View(Class);
            }
            return RedirectToAction("Index");
        }

        [HttpPatch]
        public IActionResult Edit(Class Cl)
        {
            if (ModelState.IsValid)
            {
                if (_CR.UpdateClass(Cl).GetAwaiter().GetResult())
                {
                    return RedirectToAction("Index");
                }
            }
            return View(Cl);
        }


        
        public IActionResult Delete(int id)
        {
            var Class = _CR.GetClassById(id).GetAwaiter().GetResult();
            if (Class != null)
            {
                if (_CR.DeleteClass(Class).GetAwaiter().GetResult())
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
