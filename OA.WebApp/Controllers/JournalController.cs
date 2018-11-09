using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OA.WebApp.Data;
using OA.WebApp.ViewModels;
using OA.WebApp.Models;

namespace OA.WebApp.Controllers
{
    public class JournalController : BaseController
    {
        private OAContext _context;
        private IMapper _mapper;

        public JournalController(OAContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<JournalDto> journalDtos = new List<JournalDto>();
            journalDtos = await _context.ExecuteToListAsync<JournalDto>(
                @"select * from fm_journal");
            return View(journalDtos);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult EntrySave()//[Bind("RecordDate,Summary")] JournalDto journalDto)
        {
            Journal jl = new Journal(); //_mapper.Map<Journal>(journalDto);
            jl.ID = 2;
            //if (ModelState.IsValid)
            {
                _context.Add(jl);
                _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}