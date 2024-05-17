using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    [Authorize(Roles = "ADMIN")]

    public class StoreInfoController : Controller

    {
        private readonly ICrudService<StoreInfo> _storeInfoCrudService;

        private readonly UserManager<ApplicationUser> _userManager;
        public StoreInfoController(ICrudService<StoreInfo> storeCrudSerivice,
            UserManager<ApplicationUser> userManager)
        {
            _storeInfoCrudService = storeCrudSerivice;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var storeInfoList = await _storeInfoCrudService.GetAllAsync();
            return View(storeInfoList);
        }


        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            StoreInfo storeInfo = new StoreInfo();
            if (id > 0)
            {
                storeInfo = await _storeInfoCrudService.GetAsync(id);
            }
            return View(storeInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(StoreInfo storeInfo)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                if (storeInfo.Id == 0)
                {
                    storeInfo.CreatedDate = DateTime.Now;
                    storeInfo.CreatedBy = userId;

                    await _storeInfoCrudService.InsertAsync(storeInfo);

                    TempData["success"] = "Data added successfully😀";

                }
                else
                {
                    var OrgStoreInfo = await _storeInfoCrudService.GetAsync(storeInfo.Id);
                    OrgStoreInfo.StoreName = storeInfo.StoreName;
                    OrgStoreInfo.Address = storeInfo.Address;
                    OrgStoreInfo.PhoneNumber = storeInfo.PhoneNumber;
                    OrgStoreInfo.PanNumber = storeInfo.PanNumber;
                    OrgStoreInfo.RegistrationNumber = storeInfo.RegistrationNumber;
                    OrgStoreInfo.IsActive = storeInfo.IsActive;
                    OrgStoreInfo.ModifiedBy = userId;
                    OrgStoreInfo.ModifiedDate = DateTime.Now;

                    await _storeInfoCrudService.UpdateAsync(OrgStoreInfo);

                    TempData["success"] = "Data updated successfully!😀";

                }

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Please input valid data";
            //StoreInfo stinfo=new StoreInfo();
            return View(storeInfo);

            //return RedirectToAction("AddEdit");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int userId)
        {
            // get storeId
            var storeInfo = await _storeInfoCrudService.GetAsync(userId);
            ; _storeInfoCrudService.Delete(storeInfo);
            TempData["errror"] = "Data deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}
