using System;
using System.Collections.Generic;
using System.Linq;
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
    public interface IUserService
    {
        /// <summary>
        /// 验证User的UserName和password
        /// <para>userName: 用户名</para>
        /// <para>password: 用户密码</para>
        /// </summary>
        /// <returns>user实例</returns>
        User Authenticate(string username, string password);
        IEnumerable<UserDto> GetAll();
        Task<UserDto> GetByID(int? ID);
        Task<User> Create(UserDto userDto, string password);
        Task<int> UpdateWithPassword(UserDto userDto, string password = null);
        Task<int> UpdateBy(UserDto userDto, string property = null, object value = null);
        Task<int> Delete(int? ID);
    }

    public class UserService : IUserService
    {
        private OAContext _context;
        private IMapper _mapper;

        public UserService(OAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 验证User的UserName和password
        /// <para>userName: 用户名</para>
        /// <para>password: 用户密码</para>
        /// </summary>
        /// <returns>user实例</returns>
        public User Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
                return null;

            if (!VerifyUser.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        /// <summary>
        /// 查询所有User
        /// </summary>
        /// <returns>IEnumerable users</returns>
        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IList<UserDto>>(_context.Users);
        }

        /// <summary>
        /// 查找User
        /// <para>id：User id</para>
        /// </summary>
        /// <returns>User</returns>
        public async Task<UserDto> GetByID(int? id)
        {
            return  _mapper.Map<UserDto>(await _context.Users.FirstOrDefaultAsync(m => m.ID == id));
        }

        /// <summary>
        /// 创建User
        /// <para>user：User 实例</para>
        /// <para>password：User的密码明码</para>
        /// </summary>
        public async Task<User> Create(UserDto userDto, string password)
        {
            User user = _mapper.Map<User>(userDto);

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("密码必填");

            if (_context.Users.Any(x => x.UserName == userDto.UserName))
                throw new AppException("UserName '" + userDto.UserName + "' 已经存在");

            byte[] passwordHash, passwordSalt;
            VerifyUser.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// 更新User密码
        /// <para>userParam：User 实例</para>
        /// <para>password：User的密码明码</para> 
        /// </summary>
        public async Task<int> UpdateWithPassword(UserDto userDto, string password = null)
        {
            var user = await _context.Users.FindAsync(userDto.ID);
            
            if (user == null)
                throw new AppException("没有找到User");

            _mapper.Map<UserDto, User>(userDto, user);

            // 清空passwordHash和salt
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                VerifyUser.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Update(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateBy(UserDto userDto, string property = null, Object value = null)
        {
            if (property != "password")
            {
                var user = _context.Users.FirstOrDefaultAsync(m => m.ID == userDto.ID);
                _context.Entry(user).Property(property).IsModified = true;
                return await _context.SaveChangesAsync();
            }
            else
            {
                return await this.UpdateWithPassword(userDto, (string)value);
            }
        }

        /// <summary>
        /// 删除User
        /// <para>id：User id</para>
        /// </summary>
        public async Task<int> Delete(int? id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
