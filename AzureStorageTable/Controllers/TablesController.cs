using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorageTable.Models;
using AzureStorageTable.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AzureStorageTable.Controllers
{
    public class TablesController : Controller
    {
        public IActionResult Index()
        {
            var Repository = new Todo_Repository();
            List<Tododo> Todolist = new List<Tododo>();
            var entities=Repository.GetAll();
            foreach(var item in entities)
            {
                Tododo todo = new Tododo() 
                {
                    Id=item.RowKey,
                    Group=item.PartitionKey,
                    Completed=item.Completed,
                    Item=item.Item
                };

                Todolist.Add(todo);
            }

            return View(Todolist);
        }
        [HttpGet]
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrUpdate(Tododo todo)
        
        {
            if (!ModelState.IsValid)
            {
                return View(todo);
            }

            Entities entity = new Entities()
            {
                PartitionKey = todo.Group,
                RowKey = todo.Id,
                Completed = todo.Completed,
                Item = todo.Item
            };
            var repo = new Todo_Repository();

            repo.CreateorUpdate(entity);
            return RedirectToAction("Index");
        }

        public IActionResult GetById(string rowid, string PartionKey)
        {
            var repo = new Todo_Repository();
            Entities entity =repo.GetByRowId(rowid, PartionKey);

            Tododo todo = new Tododo()
            {
                Id = entity.RowKey,
                Group=entity.PartitionKey,
                Completed=entity.Completed,
                Item=entity.Item
            };
            return View(todo);
        }

    }
}
