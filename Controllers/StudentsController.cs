using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;




using Lab4.Models.ViewModels;



namespace Lab4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }




        /*
        
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }*/



        // GET: Communities
        // Did you really check my example? You missed changing the index here
        public async Task<IActionResult> Index(int? ID)
        {
            var viewModel = new CommunityViewModel();
            // TODO: Please explain back to me by email what this is doing
            viewModel.Students = await _context.Students
                  .Include(i => i.Membership)
                    .ThenInclude(i => i.Community)
                  .AsNoTracking()
                  .OrderBy(i => i.ID)
                  .ToListAsync();

            if (ID != null)
            {
                ViewData["StudentId"] = ID;
                viewModel.CommunityMemberships = viewModel.Students.Where(
                    x => x.ID == ID).Single().Membership;
            }

            return View(viewModel);
        }


        public async Task<IActionResult> EditMemberships(string ID)
        {

     

            var viewModel = new CommunityMembersipViewModel();

            viewModel.number = ID;

           
            viewModel.Communities = await _context.Communities
                  .Include(i => i.Membership)
                    .ThenInclude(i => i.Student)
                  .AsNoTracking()
                  .OrderBy(i => i.Title)
                  .ToListAsync();




            var ep = Int16.Parse(ID);

            viewModel.Students = await _context.Students
                .Include(i => i.Membership)
                  .ThenInclude(i => i.Community)
                .AsNoTracking()
                .OrderBy(i => i.ID)
                .ToListAsync();

            if (ID != null)
            {
                
                ViewData["StudentId"] = ep;
                viewModel.CommunityMemberships = viewModel.Students.Where(
                    x => x.ID == ep).Single().Membership;
            }


            return View(viewModel);



        }




        public async Task<IActionResult> Addmembership(string COM, int ID)
        {
           
            
            
            var memberships = new CommunityMembership[]
                                  {
            new CommunityMembership{StudentID=ID,CommunityID=COM}
                                  };
            foreach (var m in memberships)
            {
                _context.CommunityMemberships.Add(m);
            }
            _context.SaveChanges();





            return NoContent();


        }


        public async Task<IActionResult> RemoveMembership(string COM,int ID)
        {

            var memberships = new CommunityMembership[]
                        {
            new CommunityMembership{StudentID=ID,CommunityID=COM}
                        };
            foreach (var m in memberships)
            {
                _context.CommunityMemberships.Remove(m);
            }
            _context.SaveChanges();


           

            return NoContent();


        }







        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
