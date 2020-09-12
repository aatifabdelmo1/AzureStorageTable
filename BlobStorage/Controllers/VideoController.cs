using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlobStorage.Models;
using BlobStorage.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;

namespace BlobStorage.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            var repo = new BlobRepo();
            var blobs=repo.GetBlob();
            List<blob> blist = new List<blob>();
            foreach (var item in blobs)
            {
                blob b = new blob()
                {
                    Name = item.Name,
                    url=repo.containerURL+"/"+item.Name
                };
                blist.Add(b);
            }
            return View(blist);
        }

        [HttpGet]
        public IActionResult UploadVideo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadVideo(List<IFormFile> file, Video video)
        {
            if (file.Count > 0)
            {
                var repo = new BlobRepo();
                string filename = Path.GetFileName(file[0].FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload",file[0].FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file[0].CopyToAsync(stream).GetAwaiter();
                    
                }
                repo.UploadBlob(filename, path);
                return RedirectToAction("Index");

            }
            return View(video);
        }

        public IActionResult Delete(string name)
        {
            var repo = new BlobRepo();
            repo.deleteblob(name);
            return RedirectToAction("Index");
        }
    }
}
