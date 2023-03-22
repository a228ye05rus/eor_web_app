using eor_web_app.Models;
using eor_web_app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace eor_web_app.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SubContext _context;
        IWebHostEnvironment _appenvironment;

        public ProfileController(SubContext subContext, IWebHostEnvironment environment)
        {
            _context = subContext;
            _appenvironment = environment;
        }
        public IActionResult Index(int? id)
        {
            //var profiles = (from p in _context.Profiles
            //               .Include(p => p.Direction)
            //                where p.DirectionId == id
            //                select p).ToList();
            //return View(profiles);
            if (id == null) return Redirect("/Direction/Index");
            Direction direction = _context.Directions.Find(id);
            ViewBag.Direction = $"{direction.Code} - {direction.Name}";

            IndexViewModel indexViewModel = new IndexViewModel
            {
                Profiles = _context.Profiles.Where(p => p.DirectionId == direction.Id),
                Groups = _context.Groups.ToList(),
                Subjects = _context.Subjects.ToList(),
                FileModels = _context.FileModels.ToList()
            };
            return View(indexViewModel);
        }

        [Authorize(Roles = "admin")]
        //create a new profile
        public IActionResult Create(int? DirectionId)
        {
            if (DirectionId == null)
            {
                return NotFound();
            }
            //var dir = _context.Directions.Find(DirectionId);
            //ViewBag.D = dir.Name;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, DirectionId")] Profile profile)
        {
            try
            {
                _context.Add(profile);
                _context.SaveChanges();
                return Redirect("/Profile/Index/" + profile.DirectionId);
            }
            catch
            { 
                return View(profile);
            }
            
        }


        [Authorize(Roles = "admin")]
        //edit a profile
        public IActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = _context.Profiles.Find(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, DirectionId")] Profile profile) 
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(profile);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!ProfileExists(profile.Id))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return Redirect("~/Profile/Index/" + profile.DirectionId);
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }

        [Authorize(Roles = "admin")]
        //delete a profile
        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = _context.Profiles.Find(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }


        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var profiles = _context.Profiles.Find(id);
            _context.Profiles.Remove(profiles);
            _context.SaveChanges();
            return Redirect("~/Profile/Index/" + profiles.DirectionId);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Show(/*int? id*/) 
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //var profiles = (from p in _context.Profiles
            //               .Include(p => p.Direction)
            //                where p.DirectionId == id
            //                select p).ToList();
            return View(_context.Profiles.ToList());
        }
    }
}
