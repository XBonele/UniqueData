using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndexTest.Entity;
using IndexTest.Models;

namespace IndexTest.Controllers
{
    public class IndexUsersController : Controller
    {
        private readonly IndexTestDBContext _context;

        public IndexUsersController()
        {
            _context = new IndexTestDBContext();
        }

        // GET: IndexUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.IndexUsers.ToListAsync());
        }

        // GET: IndexUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indexUser = await _context.IndexUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indexUser == null)
            {
                return NotFound();
            }

            return View(indexUser);
        }

        // GET: IndexUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IndexUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,FirstName,LastName,Password")] IndexUser indexUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(indexUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException e)
                {
                    ModelState.AddModelError("Alert: ",e.InnerException.Message);
                }
            }
            return View(indexUser);
        }

        // GET: IndexUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indexUser = await _context.IndexUsers.FindAsync(id);
            if (indexUser == null)
            {
                return NotFound();
            }
            return View(indexUser);
        }

        // POST: IndexUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,FirstName,LastName,Password")] IndexUser indexUser)
        {
            if (id != indexUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indexUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndexUserExists(indexUser.Id))
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
            return View(indexUser);
        }

        // GET: IndexUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indexUser = await _context.IndexUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indexUser == null)
            {
                return NotFound();
            }

            return View(indexUser);
        }

        // POST: IndexUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indexUser = await _context.IndexUsers.FindAsync(id);
            _context.IndexUsers.Remove(indexUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndexUserExists(int id)
        {
            return _context.IndexUsers.Any(e => e.Id == id);
        }
    }
}
