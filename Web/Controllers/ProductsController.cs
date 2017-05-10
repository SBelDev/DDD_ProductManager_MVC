using Core.Services;
using DAL;
using NLog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly Logger Logger = LogManager.GetLogger("ProductsController");

        private IService<Product> productService;
        private IService<Category> categoryService;
        private IService<Tag> tagService;

        public ProductsController(IService<Product> productService, IService<Category> categoryService, IService<Tag> tagService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.tagService = tagService;
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = productService.GetAll();
            return View(products.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ProductViewModel productVM = new ProductViewModel();
            productVM.Tags = PopulateSelectedTags(null);
            productVM.Categories = categoryService.GetAll().ToList();
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = productVM.Name;
                product.Quantity = productVM.Quantity;
                product.Price = productVM.Price;
                product.ShowInList = productVM.ShowInList;
                product.CategoryId = productVM.CategoryId;
                product.IsSendByEmail = false;

                foreach (var tag in productVM.Tags.Where(x=>x.Selected==true))
                {
                    var tagAdd = tagService.GetByID(tag.TagId);
                    product.Tags.Add(tagAdd);
                }

                if (productVM.File != null && productVM.File.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(productVM.File.InputStream))
                    {
                        product.Image = reader.ReadBytes(productVM.File.ContentLength);
                    }
                }

                productService.Insert(product);
                return RedirectToAction("Index");
            }
            productVM.Categories = categoryService.GetAll().ToList();
            return View("Create", productVM);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                Logger.Warn("Get product by ({ ID}), id is null", id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productService.GetByID(id);
            if (product == null)
            {
                Logger.Warn("Get product by ({ ID}) not found", id);
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Logger.Warn("Get product by ({ ID}), id is null", id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productService.GetByID(id);

            if (product == null)
            {
                Logger.Warn("Get product by ({ ID}), not found", id);
                return HttpNotFound();
            }

            ProductViewModel productVM = new ProductViewModel();

            productVM.Id = product.Id;
            productVM.Name = product.Name;
            productVM.Quantity = product.Quantity;
            productVM.Price = product.Price;
            productVM.ShowInList = product.ShowInList;
            productVM.CategoryId = product.CategoryId;
            productVM.FileInBytes = product.Image;
            productVM.Tags = PopulateSelectedTags(product);
            productVM.Categories = categoryService.GetAll().ToList();
            return View(productVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = productVM.Name;
                product.Quantity = productVM.Quantity;
                product.Price = productVM.Price;
                product.ShowInList = productVM.ShowInList;
                product.CategoryId = productVM.CategoryId;
                product.IsSendByEmail = false;

                foreach (var tag in productVM.Tags.Where(x => x.Selected == true))
                {
                    //product.Tags.Add(new Tag() { Id = tag.TagId, Color = tag.Color, Name = tag.Name });
                    var tagAdd = tagService.GetByID(tag.TagId);
                    product.Tags.Add(tagAdd);
                }

                if (productVM.File != null && productVM.File.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(productVM.File.InputStream))
                    {
                        product.Image = reader.ReadBytes(productVM.File.ContentLength);
                    }
                }
                productService.Insert(product);
                return RedirectToAction("Index");
            }
            productVM.Categories = categoryService.GetAll().ToList();
            return View("Edit", productVM);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                Logger.Warn("Get product by ({ ID}), id is null", id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productService.GetByID(id);
            if (product == null)
            {
                Logger.Warn("Get product by ({ ID}), not found", id);
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productService.GetByID(id);
            productService.Delete(product);
            return RedirectToAction("Index");
        }

        private List<SelectedTag> PopulateSelectedTags(Product product)
        {
            IEnumerable<Tag> allTags = tagService.GetAll();
            HashSet<int> productTags = new HashSet<int>();

            if (product != null && product.Tags.Any())
            {
                productTags = new HashSet<int>(product.Tags.Select(p => p.Id));
            }

            var selectedTags = new List<SelectedTag>();
            foreach (var tag in allTags)
            {
                selectedTags.Add(new SelectedTag
                {
                    TagId = tag.Id,
                    Name = tag.Name,
                    Color = tag.Color,
                    Selected = productTags.Contains(tag.Id)
                });
            }
            return selectedTags;
        }
    }
}
 