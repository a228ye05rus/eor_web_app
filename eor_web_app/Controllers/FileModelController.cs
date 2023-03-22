using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eor_web_app.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eor_web_app.Controllers
{
    public class FileModelController : Controller
    {
        private readonly SubContext _context;
        IWebHostEnvironment _appenvironment;

        public FileModelController(SubContext subContext, IWebHostEnvironment environment)
        {
            _context = subContext;
            _appenvironment = environment;
        }


        public IActionResult Index()
        {
            return View(_context.FileModels.ToList());
        }

        //get
        public IActionResult Create(int SubjectId)
        {
            var subj = _context.Subjects.FirstOrDefault(s => s.Id == SubjectId);
            ViewBag.D = subj.Name;
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Code", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile uploadedFile, int SubjectId)
        {
            if (uploadedFile != null)
            {

                FileModel f = new FileModel { SubjectId = SubjectId, Name = uploadedFile.FileName };
                string path = "/Files/" + f.OName + ".pdf";
                using (var fileStream = new FileStream(_appenvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
                f.Path = path;
                _context.FileModels.Add(f);
                _context.SaveChanges();
                return Redirect("~/Direction/Index");
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Code", "Name");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var file = _context.Subjects
                .Include(s => s.FileModel)
                .FirstOrDefault(s => s.Id == id);
            var filem = file.FileModel;
            //var asdasd = filem.OName;
            if (filem == null)
            { 
                return NotFound();
            }
            return View(filem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id) 
        {
            try
            {
                var filem = _context.FileModels.Find(id);

                string path = _appenvironment.WebRootPath + filem.Path;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists) { fileInfo.Delete(); }

                _context.FileModels.Remove(filem);
                _context.SaveChanges();
            }
            catch {
                var file = _context.Subjects
                    .Include(s => s.FileModel)
                    .FirstOrDefault(s => s.Id == id);
                var filem = file.FileModel;

                string path = _appenvironment.WebRootPath + filem.Path;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists) { fileInfo.Delete(); }

                _context.FileModels.Remove(filem);
                _context.SaveChanges();
            }
           
            return Redirect("~/Direction/Index");
        }


        public IActionResult Details(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = _context.Subjects
                .Include(s => s.FileModel)
                .FirstOrDefault(s => s.Id == id);
            var filem = file.FileModel;
            if (filem == null)
            {
                return NotFound();
            }

            return View(filem);
        }

    }
}
