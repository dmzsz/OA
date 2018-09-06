using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.ViewModels;
using OA.WebApp.Data;
using OA.WebApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using OA.Core.Exceptions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using OA.WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace OA.WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        [HttpGet]
        public IActionResult Index()
        {
            return View(_userService.GetAll());
        }

        // GET: Users/New
        // 新建用户页面
        [HttpGet]
         public IActionResult New()
        {
            return View();
        }

        // POST: Users/Create
        // 创建新用户
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password")] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.Create(userDto, userDto.Password);
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Users/Details/5
        // 用户信息展示
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Password")] UserDto userDto)
        {
            if (id != userDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userService.UpdateWithPassword(userDto, userDto.Password);
                return RedirectToAction(nameof(Index));
            }
            return View(userDto);
        }

        // GET: Users/Edit/5
        // 编辑用户信息
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userDto = await _userService.GetByID(id);
            if (userDto == null)
            {
                return NotFound();
            }
            return View(userDto);
        }


        // GET: Users/Delete/5
        // 删除用户页面
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetByID(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        // 删除用户，暂时没有用软删除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Login
        // 登陆页面
        [HttpGet]
        [AllowAnonymous]
        public  IActionResult Login(string returnUrl)
        {
            return View(new UserDto
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: Users/Login
        // 用户登陆
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            User user;
            if (ModelState.IsValid) { 
                user = _userService.Authenticate(userDto.UserName, userDto.Password);

                if (user != null)
                {
                    await LoginAsync(user);
                    return RedirectToLocal(userDto.ReturnUrl);
                }

                ModelState.AddModelError("InvalidCredentials", "用户名密码不正确");
            }
            return View(userDto);
        }


        // GET: Users/Login
        // 注册页面
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Sigup(string returnUrl)
        {
            return View(new UserDto
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: Users/register
        // 关闭对外开放
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[AllowAnonymous]
        //public async Task<IActionResult> Sigup([Bind("UserName,Password")] UserDto userDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _userService.Create(userDto, userDto.Password);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (AppException ex)
        //        {
        //            // return error message if there was an exception
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    return View(userDto);
        //}

        // GET: Users/Logout
        // POST: Users/Logout
        // 推出登陆
        [HttpGet, HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


        // private methods

        // 用户cookie登陆
        private async Task LoginAsync(User user)
        {
            var properties = new AuthenticationProperties
            {
                AllowRefresh = false,
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GivenName,  "FirstName")); // user.Employee?.FirstName
            identity.AddClaim(new Claim(ClaimTypes.Surname, "LastName")); // user.Employee?.LastName

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal, properties);
        }

        // 重定向到returnUrl
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        // 验证url是否合法
        private static bool IsUrlValid(string returnUrl)
        {
            return !string.IsNullOrWhiteSpace(returnUrl)
                   && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative);
        }

        private bool UserExists(int id)
        {
            return _userService.GetByID(id) != null;
        }


    }
}
