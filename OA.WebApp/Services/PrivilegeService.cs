using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OA.Core.Authorization;
using OA.Core.Exceptions;
using OA.WebApp.Data;
using OA.WebApp.Models;
using OA.WebApp.ViewModels;

namespace OA.WebApp.Services
{
    public interface IPrivilegeService
    {
        
        Task<IEnumerable<PrivilegeDto>> GetControllerAsync(int? id);
    }

    public class PrivilegeService : IPrivilegeService
    {
        private OAContext _context;
        private IMapper _mapper;

        public PrivilegeService(OAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        

        public async Task<IEnumerable<PrivilegeDto>> GetControllerAsync(int? id)
        {
            IEnumerable<PrivilegeDto> privilegeDtos = new List<PrivilegeDto>();
            //privilegeDtos = await _context.ExecuteToListAsync<PrivilegeDto>(
            //    @"SELECT p.ID, 
            //            p.Name, 
            //         p.Description,
            //         p.ControllerName,
            //         a.ActionNames, 
            //         p.CreatedAt, 
            //         p.CreatedBy, 
            //         p.ModifiedAt,
            //         p.ModifiedBy 
            //    FROM [st_privilege] AS p
            //    RIGHT JOIN
            //     (SELECT min(id) AS ID ,
            //       CONCAT('[', STUFF(
            //         (SELECT ',' + '[' + CONVERT(varchar(10), id) + ',''' +  ActionName +''']'
            //         FROM st_privilege AS T1
            //         WHERE T1.Name = T2.Name AND T1.ControllerName = T2.ControllerName AND T1.Deleted = 0
            //         FOR XML PATH('')) , 1, 1, ''),']') AS ActionNames
            //     FROM st_privilege AS T2
            //        WHERE T2.Deleted = 0
            //     GROUP BY Name, ControllerName) AS a 
            //    ON a.id = p.id
            //    ORDER BY Name, ControllerName;");

            //privilegeDtos = await _context.Privileges
            //        .GroupBy(p => new { p.Name, p.ControllerName })
            //        .Where(gd => gd.Select(p => p.Deleted == false).Any(x => x))
            //        .Select(gd => new PrivilegeDto()
            //        {
            //            ID = gd.Where(p => p.Deleted == false).Select(p => p.ID).First(),
            //            Name = gd.Key.Name,
            //            Description = gd.First().Description,
            //            //ControllerName = gd.Key.ControllerName,
            //            ActionNames = "["
            //                            + string.Join(",",
            //                                            gd.Where(p => p.Deleted == false)
            //                                            .Select(p => "[" + p.ID + ",'" + p.ActionName + "']")
            //                                            .ToList())
            //                           + "]",
            //            Actions = gd.ToList(),
            //            CreatedAt = gd.First().CreatedAt,
            //            CreatedBy = gd.First().CreatedBy,
            //            ModifiedAt = gd.First().ModifiedAt,
            //            ModifiedBy = gd.First().ModifiedBy
            //        })
            //        .ToListAsync();

            List<Privilege> privileges = new List<Privilege>();
            if (id != null)
            {
                privileges.Add(await _context.Privileges
                                    .FirstOrDefaultAsync(m => m.ID == id));
            }
            else
            {
                privileges = await _context.Privileges.ToListAsync();
            }
            
            privilegeDtos = from p in privileges
                            where p.Deleted == false
                            orderby p.Name, p.ControllerName, p.ControllerName
                            group p by p.Name into g
                            select new PrivilegeDto()
                            {
                                ID = g.First<Privilege>().ID,
                                Name = g.First<Privilege>().Name,
                                Description = g.First<Privilege>().Description,
                                Controllers = (from p1 in g
                                               group p1 by p1.ControllerName into g1
                                               select new Dictionary<Privilege, IEnumerable<Privilege>>
                                               {
                                                   {
                                                       g1.First<Privilege>(), // controller name
                                                       g1.ToList() //action list
                                                   }
                                               }
                                               ),
                                CreatedAt = g.First<Privilege>().CreatedAt,
                                CreatedBy = g.First<Privilege>().CreatedBy,
                                ModifiedAt = g.First<Privilege>().ModifiedAt,
                                ModifiedBy = g.First<Privilege>().ModifiedBy

                            };
            return privilegeDtos;
        }

    }
}
