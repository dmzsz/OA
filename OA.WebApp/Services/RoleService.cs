using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OA.Core.Exceptions;
using OA.WebApp.Data;
using OA.WebApp.Models;
using OA.WebApp.ViewModels;

namespace OA.WebApp.Services
{
    public interface IRoleService
    {
        Task<Role> Create(RoleDto roleDto);
        IEnumerable<RoleDto> GetAll();
        Task<RoleDto> GetByID(int? ID);
        Task<RoleDto> GetByUserID(int? ID);
        Task<RoleDto> GetByName(int? ID);
        Task<int> UpdateBy(RoleDto roleDto, string property = null, object value = null);
        Task<int> Delete(int? ID);
    }

    public class RoleService : IRoleService
    {
        private OAContext _context;
        private IMapper _mapper;

        public RoleService(OAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 创建Role
        /// <para>role：Role 实例</para>
        /// <para>password：Role的密码明码</para>
        /// </summary>
        public async Task<Role> Create(RoleDto roleDto)
        {
            Role role = _mapper.Map<Role>(roleDto);

            if (_context.Roles.Any(x => x.Name == roleDto.Name))
                throw new AppException("RoleName '" + roleDto.Name + "' 已经存在");

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        /// <summary>
        /// 查询所有Role
        /// </summary>
        /// <returns>IEnumerable roles</returns>
        public IEnumerable<RoleDto> GetAll()
        {
            return _mapper.Map<IList<RoleDto>>(_context.Roles);
        }

        /// <summary>
        /// 查找Role
        /// <para>id：Role id</para>
        /// </summary>
        /// <returns>Role</returns>
        public async Task<RoleDto> GetByID(int? id)
        {
            return _mapper.Map<RoleDto>(await _context.Roles.FirstOrDefaultAsync(m => m.ID == id));
        }

        /// <summary>
        /// 查找Role
        /// <para>id：Role id</para>
        /// </summary>
        /// <returns>Role</returns>
        public async Task<RoleDto> GetByUserID(int? id)
        {
            return _mapper.Map<RoleDto>(_context.Roles.Include(r => r.UserRoles).ThenInclude(r => r.User.ID == id).ToList());
        }

        /// <summary>
        /// 查找Role
        /// <para>id：Role id</para>
        /// </summary>
        /// <returns>Role</returns>
        public async Task<RoleDto> GetByName(int? id)
        {
            return _mapper.Map<RoleDto>(await _context.Roles.FirstOrDefaultAsync(m => m.ID == id));
        }




        public async Task<int> UpdateBy(RoleDto roleDto, string property = null, Object value = null)
        {

            var role = _context.Roles.FirstOrDefaultAsync(m => m.ID == roleDto.ID);
            _context.Entry(role).Property(property).IsModified = true;
            return await _context.SaveChangesAsync();

        }

        /// <summary>
        /// 删除Role
        /// <para>id：Role id</para>
        /// </summary>
        public async Task<int> Delete(int? id)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(m => m.ID == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
