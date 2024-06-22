using Apil_PMS.Infrastructure.IRepository.ICrudService;
using Apil_PMS.Models.Entity;
using Apil_PMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Apil_PMS.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ICrudService<Product> _productCrudService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<Category> _categoryCrudService;

        public ProductController(ICrudService<Product> productCrudService, UserManager<ApplicationUser> usermanager, ICrudService<Category> categoryCrudService)

        {
            _productCrudService = productCrudService;
            _userManager = usermanager;
            _categoryCrudService = categoryCrudService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var productList = await _productCrudService.GetAllAsync();
            return View(productList);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {

            Product product = new Product();
            ViewBag.CategoryInfos = await _categoryCrudService.GetAllAsync(p => p.IsAvaliable == true);
            product.IsAvaliable = true;
            if (Id > 0)
            {
                product = await _productCrudService.GetAsync(Id);
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product)
        {
            try
            {
                var user = _userManager.GetUserId(HttpContext.User);

                if (product.ProductImage != null)
                {
                    string fileDirectory = $"wwwroot/Images";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + product.ProductImage.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/Images"), uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ProductImage.CopyToAsync(fileStream);
                        product.ImageUrl = $"Images/" + uniqueFileName;

                    }

                }
                if (product.Id == 0)
                {
                    product.CategoryId = product.CategoryId;
                    product.CreatedDate = DateTime.Now;
                    product.CreatedBy = user;
                    await _productCrudService.InsertAsync(product);
                    TempData["success"] = "Data Added Successfully";
                }
                else
                {
                    var productInfo = await _productCrudService.GetAsync(product.Id);
                    productInfo.ProductName = product.ProductName;
                    productInfo.ProductDescription = product.ProductDescription;
                    productInfo.Rate = product.Rate;
                    productInfo.CategoryId = product.CategoryId;
                    productInfo.BatchNo = product.BatchNo;
                    productInfo.ProductionDate = product.ProductionDate;
                    productInfo.ExpiryDate = product.ExpiryDate;
                    productInfo.IsAvaliable = product.IsAvaliable;

                    await _productCrudService.UpdateAsync(productInfo);
                    TempData["success"] = "Data Updated Successfully";


                }


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(AddEdit));
            }

        }




        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productCrudService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productCrudService.DeleteAsync(product);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(Index));


        }
    }
}
