using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.DBModels;
using App.Models;
using Bogus;
using DAFW_IS220.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly MyShopContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManageController(MyShopContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Administrator)]
        public IActionResult DeleteDb()
        {
            return View();
        }

        

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> DeleteDbAsync()
        {


            var success = await _dbContext.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Xóa Database thành công" : "Không xóa được Db";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();

            StatusMessage = "Cập nhật Database thành công";

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> SeedDataAsync()
        {
            // Create Roles
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }

            // admin, pass=admin123, 21521974@gm.uit.edu.vn
            var useradmin = await _userManager.FindByEmailAsync("21521974@gm.uit.edu.vn");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "21521974@gm.uit.edu.vn",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);

                return RedirectToAction("SeedData");

            }
            else
            {
                var user = await _userManager.GetUserAsync(this.User);
                if (user == null) return this.Forbid();
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Any(r => r == RoleName.Administrator))
                {
                    return this.Forbid();
                }

            }



            StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");


        }
      
}
}