using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Threading.Tasks;
namespace SportsStore.Areas.Administration.Controllers
{
    public class AdminController : Controller
    {
        // GET: Administration/Admin
        private IProductRepository repository;
        //List<SelectListItem> list1 = new List<SelectListItem>(Enum.GetNames(System.Type.GetType(Category)));
        private List<SelectListItem> list; 
             
            
        public AdminController(IProductRepository repo)
        {
            repository = repo;
            list = new List<SelectListItem>();
            foreach(var item in Enum.GetNames(typeof(Category)))
            {
                list.Add(new SelectListItem() { Text = item, Value = item });
            }
            ViewBag.Category = list;
        }
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ViewResult Create()
        {
           // ViewData["Category"] = list;
            return View("Edit", new Product());
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            foreach(var item in list)
            {
                if (product.Category == item.Text)
                    item.Selected = true;
            }
            
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Edit(Product product,HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}