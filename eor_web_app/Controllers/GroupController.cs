using eor_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace eor_web_app.Controllers
{
    public class GroupController : Controller
    {

        private readonly SubContext _context;
        IWebHostEnvironment _appenvironment;

        public GroupController(SubContext subContext, IWebHostEnvironment environment)
        {
            _context = subContext;
            _appenvironment = environment;
        }

        public IActionResult Index(int? id)
        {
            var group = (from p in _context.Groups
                           .Include(p => p.Profile)
                            where p.ProfileId == id
                            select p).ToList();
            return View(group);
        }


        [Authorize(Roles = "admin")]
        //create a new group
        public IActionResult Create(int? ProfileId)
        {
            if (ProfileId == null) 
            {
                return NotFound();
            }
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, ProfileId")] Group group)
        {
            try
            {
                _context.Add(group);
                _context.SaveChanges();
                return Redirect("~/Direction/Index");
            }
            catch
            {
                return View(group);
            }
        }


        [Authorize(Roles = "admin")]
        //edit a group
        public IActionResult Edit(int? id) 
        {
            if (id == null)
                return NotFound();

            var group = _context.Groups.Find(id);

            if (group == null)
                return NotFound();

            return View(group);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, ProfileId")] Group group) 
        {
            if (id != group.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(group);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.Id))
                    return NotFound();
                else
                    throw;
            }
            return Redirect("~/Profile/Index" + group.ProfileId);
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }

        [Authorize(Roles = "admin")]
        //delete a proup
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }


        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var group = _context.Groups.Find(id);
            _context.Groups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
