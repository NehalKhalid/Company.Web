﻿using Company.Data.Models;
using Company.Service.Services.Department.Dto;
using Company.Service.Services.Employee.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUser> _logger;

        public UserController(UserManager<ApplicationUser> userManager,ILogger<ApplicationUser> logger)
        {
			_userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string searchInp)
		{
			IEnumerable<ApplicationUser> users;
			if (string.IsNullOrEmpty(searchInp))
				users = await _userManager.Users.ToListAsync();	
			else
				users = await _userManager.Users.Where(user=> user.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper())).ToListAsync();
			return View(users);
		}
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            if (viewName == "Update")
            {
                var userViewModel = new UpdateUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                return View(viewName,userViewModel);
            }
            return View(viewName, user);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateUserViewModel applicationUser)
        {
            if (applicationUser.Id != id)
                return NotFound();

            if (ModelState.IsValid) 
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();
                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction("Index");
                    }
                        
                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);

                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                }
            }

            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var item in result.Errors)
                    _logger.LogError(item.Description);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

            }
            return RedirectToAction("Index");
        }
    }
}