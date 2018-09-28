using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OA.WebApp.Data;
using OA.WebApp.Models;
using OA.WebApp.ViewModels;

namespace OA.WebApp.Controllers
{
    public class T1DataController : Controller
    {
        private readonly T1Context _context;

        public T1DataController(T1Context _context)
        {
            this._context = _context;
        }

        // GET: Vessels
        // GET: Vessels/Index
        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        public IActionResult Index()
        {
            List<T1DataDto> list = new List<T1DataDto>();
            MySqlDataReader reader = _context.GetDataReader("SELECT ID FROM t_spl_freight_fcl limit 10;");
            while (reader.Read())
            {
                list.Add(new T1DataDto()
                {
                    ID = reader.GetInt16("ID")
                });

            }
            ViewData["Greeting"] = list;
            return View();
        }

    }
}
