using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HocWeb.DAO;
using HocWeb.Models;
using HocWeb.Areas.Admin.Code;
using System.Web.Mvc;


namespace HocWeb.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
       public ActionResult Index()
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging();
            return View(model);
        }
        public ActionResult XuatSanPham()
        {
            ProductDao dao = new ProductDao();
            List<ProductModels> products = dao.GetListProduct();
            SetViewBag1();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            SetViewBag1();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var product = new ProductDao().ViewDetail(ID);
            SetViewBag1();
            SetViewBag();
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductModels product)
        {
            var dao =  new ProductDao();
            var result = dao.CheckProduct(product.Name);
            if (ModelState.IsValid)
            {
                if (result ==false)
                {                 
                    var session = (UserSession)Session[CommomConstants.USER_SESSION];
                    var data = new ProductDao();
                    if (product.PromotionPrice == null)
                        product.PromotionPrice = "0";
                    product.CreatedDate = DateTime.Now;
                    product.CreatedBy = session.TenTK;
                    product.ModifiedDate = DateTime.Now;
                   product.ModifiedBy = session.TenTK;
                    var id=data.Insert(product);
                    if (id ==true)
                    {
                        SetAlert("Thêm sản phẩm thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("Thêm sản phẩm thất bại", "error");
                    }                   
                }
                else
                {
                    SetAlert("Sản phẩm đã tồn tại", "error");
                }
            }
            SetViewBag();
            SetViewBag1();
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductModels product,string id)
        {
            if (ModelState.IsValid)
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];

                var data = new ProductDao();
                if (product.PromotionPrice == null)
                    product.PromotionPrice = "0";
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = session.TenTK;
                var result = data.update(product,id);
                if (result==true)
                {
                   SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Sửa không thành công", "error");
                }
            }
            SetViewBag();
            SetViewBag1();
            return View(product);
        }
        public void SetViewBag(long? selectedID=null)
        {
            var dao = new CateProductDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name",selectedID);
        }
        public void SetViewBag1(long? selectedID = null)
        {
            var dao = new ProductDao();
            ViewBag.BrandID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
        public ActionResult Delete(string ID)
        {
            var oders = new OrderDao();
            bool result = oders.hasOrderDetail(ID);
            if (result == false)
            {
                SetAlert("Xoá thất bại !!!", "error");
            }
            else
            {
                new ProductDao().Delete(ID);
                SetAlert("Xóa thành công", "success");
            }                      
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public ActionResult DashBoard()
        {
            var dao = new ProductDao();
            var list = dao.ListAllProduct();
            List<int> reparttitons = new List<int>();
            var posion = list.Select(x => x.Price).Distinct();
            foreach (var item in posion)
            {
                reparttitons.Add(list.Count(x => x.Price == item));
            }

            var rep = reparttitons;
            ViewBag.POSI = posion;
            ViewBag.REP = reparttitons.ToList();
            return View();
        }
        public ActionResult Detail(string id)
        {
            var dao = new ProductDao();
            var result = dao.ViewDetail(id);
            ViewBag.BrandName = dao.GetBrand_Name(result);
            ViewBag.CateProductName = dao.GetCate_Name(result);
            return View(result);
        }

    }
}