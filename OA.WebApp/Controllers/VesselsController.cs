using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Data;
using OA.WebApp.Models;

namespace OA.WebApp.Controllers
{
    [Authorize]
    public class VesselsController : BaseController
    {
        private readonly OAContext _context;

        public VesselsController(OAContext context)
        {
            _context = context;
        }

        // GET: Vessels
        // GET: Vessels/Index
        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            //_context.Vessels.ToListAsync()
            IQueryable<Vessel> vesselsIQ = from v in _context.Vessels
                                            select v;

            return View(await _context.Vessels.OrderByDescending(s => s.ETD).ToListAsync());
        }

        // GET: Vessels/5/Details
        [HttpGet("[controller]/{id:int}")]
        [HttpGet("[controller]/{id:int}/[action]")]
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

        // Get: Vessels/Show
        [HttpGet("[controller]/[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Show()
        {
            //_context.Vessels.ToListAsync()
            IQueryable<Vessel> vesselsIQ = from v in _context.Vessels
                                           select v;

            return View(await _context.Vessels.OrderBy(v => v.ETD).Where(v => v.ETD == null || v.ETD >= DateTime.Now.Date).ToListAsync());
        }

        // GET: Vessels/Create
        [HttpGet("[controller]/[action]")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vessels/Create
        [HttpPost("[controller]/[action]")]
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

        // GET: Vessels/5/Edit
        [HttpGet("[controller]/{id:int}/[action]")]
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
        [HttpPost("[controller]/{id:int}/[action]")]
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

        // GET: Vessels/5/Delete
        [HttpGet("[controller]/{id:int}/[action]")]
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

        // POST: Vessels/5/Delete
        [HttpPost("[controller]/{id:int}/Delete"), ActionName("Delete")]
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
