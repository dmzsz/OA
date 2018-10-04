﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Data;
using OA.WebApp.Models;

namespace OA.WebApp.Controllers
{
    public class PrivilegesController : Controller
    {
        private readonly OAContext _context;

        public PrivilegesController(OAContext context)
        {
            _context = context;
        }

        // GET: Privileges
        // GET: Privileges/Index
        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Privileges.ToListAsync());
        }

        // GET: Privileges/Details/5
        [HttpGet("[controller]/{id:int}")]
        [HttpGet("[controller]/{id:int}/[action]")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (privilege == null)
            {
                return NotFound();
            }

            return View(privilege);
        }

        // 分配新的权限给角色
        [HttpGet("Roles/{id:int}/[controller]/[action]")]
        public async Task<IActionResult> New(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Role role;
            role = await _context.Roles
                        .Include(r => r.RolePrivileges)
                            .ThenInclude(rp => rp.Privilege)
                        .FirstOrDefaultAsync(r => r.ID == id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Privileges/Create
        [HttpGet("[controller]/[action]")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Privileges/Create
        [HttpPost("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,ControllerName,ActionName,ScopeEnable,Deleted")] Privilege privilege)
        {
            if (ModelState.IsValid)
            {
                _context.Add(privilege);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(privilege);
        }

        // GET: Privileges/5/Edit
        [HttpGet("[controller]/{id:int}/[action]")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges.FindAsync(id);
            if (privilege == null)
            {
                return NotFound();
            }
            return View(privilege);
        }

        [HttpPost("[controller]/{id:int}/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,ControllerName,ActionName,ScopeEnable,Deleted")] Privilege privilege)
        {
            if (id != privilege.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privilege);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivilegeExists(privilege.ID))
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
            return View(privilege);
        }

        // GET: Privileges/5/Delete
        [HttpGet("[controller]/{id:int}/[action]")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilege = await _context.Privileges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (privilege == null)
            {
                return NotFound();
            }

            return View(privilege);
        }

        // POST: Privileges/5/Delete
        [HttpPost("[controller]/{id:int}/Delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var privilege = await _context.Privileges.FindAsync(id);
            _context.Privileges.Remove(privilege);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrivilegeExists(int id)
        {
            return _context.Privileges.Any(e => e.ID == id);
        }
    }
}
