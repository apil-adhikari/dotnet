using Apil_PMS.Infrastructure.IRepository.ICrudService;
using Apil_PMS.Models.Entity;
using Apil_PMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Apil_PMS.Web.Controllers
{
    [Authorize(Roles = "SUPERADMIN")]
    public class CategoryController : Controller
    {
        private readonly ICrudService<Category> _categoryCrudService;
        private readonly UserManager<ApplicationUser> _userManager;


        public CategoryController(ICrudService<Category> categoryCrudService, UserManager<ApplicationUser> userManager)
        {
            _categoryCrudService = categoryCrudService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var categoryList = await _categoryCrudService.GetAllAsync();
            return View(categoryList);
        }

        public async Task<IActionResult> AddEdit(int Id)
        {
            Category category = new Category();
            category.IsAvaliable = true;
            if (Id > 0)
            {
                category = await _categoryCrudService.GetAsync(Id);
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Category category)
        {
            try
            {
                var user = _userManager.GetUserId(HttpContext.User);

                if (category.Id == 0)
                {
                    category.CreatedDate = DateTime.Now;
                    category.CreatedBy = user;
                    await _categoryCrudService.InsertAsync(category);
                    TempData["success"] = "Data Added Successfully";
                }
                else
                {
                    var categoryInfo = await _categoryCrudService.GetAsync(category.Id);
                    categoryInfo.CategoryName = category.CategoryName;
                    categoryInfo.CategoryDescription = category.CategoryDescription;
                    categoryInfo.IsAvaliable = category.IsAvaliable;
                    categoryInfo.ModifiedBy = user;
                    categoryInfo.ModifiedDate = DateTime.Now;

                    await _categoryCrudService.UpdateAsync(categoryInfo);
                    TempData["success"] = "Data Updated Successfully";

                }

                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                TempData["error"] = "Insert Data Properly";
                return RedirectToAction(nameof(AddEdit));
            }
        }




        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = await _categoryCrudService.GetAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    await _categoryCrudService.DeleteAsync(category);
        //    TempData["error"] = "Data Deleted Successfully";
        //    return RedirectToAction(nameof(Index));


        //}


        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryCrudService.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryCrudService.DeleteAsync(category);
                TempData["message"] = "Data Deleted Successfully"; // Use "message" for success
            }
            catch (DbUpdateException ex)
            {
                // Check for foreign key constraint violation
                if (ex.InnerException is SqlException sqlEx &&
                    sqlEx.Message.Contains("REFERENCE constraint"))
                {
                    TempData["error"] = "Cannot delete category as it has related data. Consider deleting related data first.";

                }
                else
                {
                    // Handle other potential exceptions (optional)
                    // Log the exception for further investigation
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
