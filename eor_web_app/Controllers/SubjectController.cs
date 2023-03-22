using eor_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eor_web_app.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubContext _context;
        IWebHostEnvironment _appenvironment;

        public SubjectController(SubContext subContext, IWebHostEnvironment environment)
        {
            _context = subContext;
            _appenvironment = environment;
        }
        public IActionResult Index(int? id)
        {
            var subjects = (from p in _context.Subjects
                           .Include(p => p.Group)
                            where p.GroupId == id
                            select p).ToList();
            return View(subjects);
        }


        //create a new subject
        public IActionResult Create(int? GroupId)
        {
            if (GroupId == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name,Code,GroupId")] Subject subject)
        {
            try
            {
                _context.Add(subject);
                _context.SaveChanges();
                return Redirect("~/Direction/Index/" + subject.GroupId);
            }
            catch 
            {
                return View(subject);
            }
        }



        //edit 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id, Name, GroupId")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(subject);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.Id))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return RedirectToAction("Index");
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }



        //delete 

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var subject = _context.Subjects.Find(id);
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return Redirect("~/Profile/Index/" + subject.GroupId);
        }

    }
}
