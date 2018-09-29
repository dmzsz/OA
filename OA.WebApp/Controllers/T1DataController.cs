using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
        public async Task<IActionResult> Show()
        {
            List<T1DataDto> list = await getT1ListAsync();

            return View(list);
        }

        public async Task<List<T1DataDto>> getT1ListAsync(int limit = 10)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(2000);
                List<T1DataDto> list = new List<T1DataDto>();
                IDataReader reader = _context.GetDataReader(@"SELECT a.ID,a.FCL_ID,
                                                          (SELECT NAME_CN
                                                           FROM b_port
                                                           WHERE ID = a.START_PLACE_ID) AS StartNameCN,
                                                          (SELECT NAME_EN
                                                           FROM b_port
                                                           WHERE ID = a.START_PLACE_ID) AS StartNameEN,
                                                          (SELECT NAME_CN
                                                           FROM b_port
                                                           WHERE ID = a.PORT_END_ID) AS EndNameCN,
                                                          (SELECT NAME_EN
                                                           FROM b_port
                                                           WHERE ID = a.PORT_END_ID) AS EndNameEN,
                                                               a.TRANSFER_FLAG,
                                                               IFNULL((SELECT NAME_CN
                                                                         FROM b_port
                                                                         WHERE ID = a.TRANSFER_ID),'') AS TransferNameCN,
                                                               IFNULL((SELECT NAME_EN
                                                                         FROM b_port
                                                                         WHERE ID = a.TRANSFER_ID),'') AS TransferNameEN,

                                                          (SELECT LOGO
                                                           FROM b_shipping
                                                           WHERE ID = a.SHIPPING_ID) AS ShippingLogo,
                                                               IFNULL(a.ROUTE_IMG,'') AS ROUTE_IMG,
                                                               a.SCHEDULE,
                                                               a.CUTOFF_DAY,
                                                               a.SPACE_STATUS,
                                                               IFNULL(a.SPACE_TOTAL,0) AS SPACE_TOTAL,
                                                               IFNULL(a.SPACE_RESIDUE,0) AS SPACE_RESIDUE,
                                                               a.PRICE_20,
                                                               a.PRICE_40,
                                                               a.PRICE_40HQ,
                                                               a.BEGIN_DATE,
                                                               a.END_DATE,
                                                               IFNULL(a.DESCRIPTION,'') AS DESCRIPTION,
                                                               a.DISPLAY_DATE,
                                                               IFNULL((SELECT GROUP_CONCAT(DATE_FORMAT(BEGIN_DATE,'%b %d'))
                                                                         FROM t_freight_fcl_history
                                                                         WHERE SIGN = a.SIGN
                                                                           AND BEGIN_DATE BETWEEN DATE_ADD(NOW(),INTERVAL -8 WEEK) AND NOW()
                                                                         GROUP BY SIGN),'') AS BEGINDATE,
                                                               IFNULL((SELECT GROUP_CONCAT(PRICE_20)
                                                                         FROM t_freight_fcl_history
                                                                         WHERE SIGN =a.SIGN
                                                                           AND BEGIN_DATE BETWEEN DATE_ADD(NOW(),INTERVAL -8 WEEK) AND NOW()
                                                                         GROUP BY SIGN),'') AS 20GP,
                                                               IFNULL((SELECT GROUP_CONCAT(PRICE_40)
                                                                         FROM t_freight_fcl_history
                                                                         WHERE SIGN =a.SIGN
                                                                           AND BEGIN_DATE BETWEEN DATE_ADD(NOW(),INTERVAL -8 WEEK) AND NOW()
                                                                         GROUP BY SIGN),'') AS 40GP,
                                                               IFNULL((SELECT GROUP_CONCAT(PRICE_40HQ)
                                                                         FROM t_freight_fcl_history
                                                                         WHERE SIGN =a.SIGN
                                                                           AND BEGIN_DATE BETWEEN DATE_ADD(NOW(),INTERVAL -8 WEEK) AND NOW()
                                                                         GROUP BY SIGN),'') AS 40HC
                                                        FROM t_freight_fcl a
                                                        WHERE RULE_TYPE = 1
                                                          AND DISPLAY_FLAG = 1
                                                          AND REMOVE = 0
                                                          AND a.END_DATE >= NOW()
                                                        ORDER BY a.END_DATE LIMIT 1");
                while (reader.Read())
                {
                    T1DataDto t1 = new T1DataDto();

                    t1.ID = reader.GetInt32(0);
                    t1.StartNameCN = reader.GetString(2);
                    t1.StartNameEN = reader.GetString(3);
                    t1.EndNameCN = reader.GetString(4);
                    t1.EndNameEN = reader.GetString(5);
                    t1.TransferFlag = reader.GetInt32(6);
                    t1.TransferNameCN = reader.GetString(7);
                    t1.TransferNameEN = reader.GetString(8);
                    t1.ShippingLogo = reader.GetString(9);
                    t1.RouteImg = "route/pictrue/ecf82c0044824e6f988e9756c52d6003.png";// reader.GetString(10);
                    t1.SpaceStatus = reader.GetInt32(13);
                    t1.SpaceTotal = reader.GetInt64(14);
                    t1.SpaceResidue = reader.GetInt64(15);
                    t1.Price20 = reader.GetInt32(16);
                    t1.Price40 = reader.GetInt32(17);
                    t1.Price40HQ = reader.GetInt32(18);
                    t1.Description = reader.GetString(21);

                    t1.PriceDates = reader.IsDBNull(23) ? new string[] { } : reader.GetString(23).Split(",");
                    t1.GP20 = reader.IsDBNull(24) ? new int[] { } : Array.ConvertAll(reader.GetString(24).Split(","), int.Parse);
                    t1.GP40 = reader.IsDBNull(25) ? new int[] { } : Array.ConvertAll(reader.GetString(25).Split(","), int.Parse);
                    t1.HC40 = reader.IsDBNull(26) ? new int[] { } : Array.ConvertAll(reader.GetString(26).Split(","), int.Parse);

                    list.Add(t1);

                }
                return list;
            });
        }

    }
}
