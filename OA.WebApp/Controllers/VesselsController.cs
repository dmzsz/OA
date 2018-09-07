using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Data;
using OA.WebApp.Models;

namespace OA.WebApp.Controllers
{
    public class VesselsController : Controller
    {
        private readonly OAContext _context;

        public VesselsController(OAContext context)
        {
            _context = context;
        }

        // GET: Vessels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vessels.ToListAsync());
        }

        // GET: Vessels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vessel == null)
            {
                return NotFound();
            }

            return View(vessel);
        }

        // GET: Vessels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vessels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,LocalName,Voy,Port,OpDate,CloseDate,ETD,Trade")] Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vessel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vessel);
        }

        // GET: Vessels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels.FindAsync(id);
            if (vessel == null)
            {
                return NotFound();
            }
            return View(vessel);
        }

        // POST: Vessels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LocalName,Voy,Port,OpDate,CloseDate,ETD,Trade")] Vessel vessel)
        {
            if (id != vessel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vessel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VesselExists(vessel.ID))
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
            return View(vessel);
        }

        // GET: Vessels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vessel == null)
            {
                return NotFound();
            }

            return View(vessel);
        }

        // POST: Vessels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vessel = await _context.Vessels.FindAsync(id);
            _context.Vessels.Remove(vessel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VesselExists(int id)
        {
            return _context.Vessels.Any(e => e.ID == id);
        }
    }
}
