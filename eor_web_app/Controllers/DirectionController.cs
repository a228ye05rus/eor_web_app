using eor_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace eor_web_app.Controllers
{
    public class DirectionController : Controller
    {
        private readonly SubContext _context;
        IWebHostEnvironment _appenvironment;

        public DirectionController(SubContext subContext, IWebHostEnvironment environment)
        {
            _context = subContext;
            _appenvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Directions.ToList());
        }

        [Authorize(Roles = "admin")]
        //create a new direction of study
        public IActionResult Create() 
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, Code")] Direction direction) 
        {
            try 
            {
                _context.Add(direction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch 
            { 
                return View();
            }
            
        }

        [Authorize(Roles = "admin")]
        //edit a direction
        public IActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var direction = _context.Directions.Find(id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Code, Name")] Direction direction) 
        {
            if (id != direction.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(direction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectionExists(direction.Id))
                    return NotFound();
                else
                    throw;
            }
            return View(direction);
        }

        private bool DirectionExists(int id)
        {
            return _context.Directions.Any(e => e.Id == id);
        }


        [Authorize(Roles = "admin")]
        //delete a direction
        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var direction = _context?.Directions.Find(id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var direction = _context.Directions.Find(id);
            _context.Directions.Remove(direction);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
