using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Data;
using OA.WebApp.Helpers;
using OA.WebApp.Models;

namespace OA.WebApp.Controllers
{
    public class JournalsController : BaseController
    {
        private readonly OAContext _context;

        public JournalsController(OAContext context)
        {
            _context = context;
        }

        // GET: Journals
        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            string selectSql = @"SELECT * FROM fm_Journal where deleted = 0";
            SqlDataReader reader = await SqlHelper.ExecuteReaderAsync(_context, selectSql, CommandType.Text);
            IEnumerable<Journal> list = SqlHelper.ReaderToListAsync<Journal>(reader).Result;
            return View(list);
            //return View(await _context.Journals.ToListAsync());
        }

        // GET: Journals/Details/5
        [HttpGet("[controller]/{id:int}")]
        [HttpGet("[controller]/{id:int}/[action]")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var journal = await _context.Journals
            //    .FirstOrDefaultAsync(m => m.ID == id);
            string selectSql = @"SELECT * FROM fm_Journal where id = @id";
            SqlParameter parameterId = new SqlParameter("@id", SqlDbType.Int);
            parameterId.Value = id;
            SqlDataReader reader = await SqlHelper.ExecuteReaderAsync(_context, selectSql, CommandType.Text, parameterId);
            Journal journal = SqlHelper.ReaderToListAsync<Journal>(reader).Result.FirstOrDefault();

            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // GET: Journals/Create
        [HttpGet("[controller]/[action]")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RecordDate,Summary,ClientID,Amount,Borrowing,SalesAmount,Paytype,PayDate,ReceiptNo,AccountType,Deleted")] Journal journal)
        {
            if (ModelState.IsValid)
            { 
                string insertSql = @"INSERT INTO fm_Journal (
                                        RecordDate,  Summary,  ClientID,  Amount,  Borrowing,  SalesAmount,    Paytype,  PayDate,  ReceiptNo,  AccountType,  Deleted)
                                     VALUES (
                                        @RecordDate, @Summary, @ClientID, @Amount, @Borrowing, @SalesAmount,  @Paytype, @PayDate, @ReceiptNo, @AccountType, @Deleted)";

                try
                {
                   await SqlHelper.ExecuteNonQueryAsync(_context, insertSql, CommandType.Text, SqlHelper.setParamsFrom(journal));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Caught exception: " + ex.Message);
                }

                //_context.Add(journal);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(journal);
        }

        // GET: Journals/Edit/5
        [HttpGet("[controller]/{id:int}")]
        [HttpGet("[controller]/{id:int}/[action]")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RecordDate,Summary,ClientID,Amount,Borrowing,SalesAmount,Paytype,PayDate,ReceiptNo,AccountType,Deleted")] Journal journal)
        {
            if (id != journal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExists(journal.ID))
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
            return View(journal);
        }

        // GET: Journals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journal = await _context.Journals.FindAsync(id);
            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalExists(int id)
        {
            return _context.Journals.Any(e => e.ID == id);
        }
    }
}
