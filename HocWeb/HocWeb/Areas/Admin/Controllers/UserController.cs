using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HocWeb.Areas.Admin.Code;
using HocWeb.Areas.Admin.Models;
using HocWeb.Models;
using HocWeb.DAO;

namespace HocWeb.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        // GET: Admin/User
        public ActionResult Index()
        {
            var dao = new USERDAO();
            var model = dao.ListAllPaging();
            return View(model);
        }
        [HttpGet]

        public ActionResult ChangePassword()
        {         
            return View();
        }
   
        [HttpGet]
        public ActionResult Detail(string id)
        {
            var result = new USERDAO().ViewDetail(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Detail(UserModels user)
        {
            if (ModelState.IsValid)
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];
                var dao = new USERDAO();
                if (!string.IsNullOrEmpty(user.Passwords))
                {
                    var encrytedMd5 = Encryptor.MD5Hash(user.Passwords);
                    user.Passwords = encrytedMd5;
                    user.ModifiedBy = session.TenTK;
                    user.ModifiedDate = DateTime.Now;
                    user.Position = session.ChucVu;
                }
                var result = dao.UpdateDetail(user);
                if (result)
                {
                    SetAlert("Chỉnh sửa thành công", "success");
                }
                else
                {
                    SetAlert("Cập nhật tài khoản không thành công", "error");
                }

            }
            return View(user);

        }
        [HttpGet]
        public ActionResult Profilee()
        {
            var session = (UserSession)Session[CommomConstants.USER_SESSION];
            var user = new USERDAO().ViewDetail(session.UserID);
            return View(user);
        }
        [HttpPost]
        public ActionResult Profilee(UserModels user)
        {
            if (ModelState.IsValid)
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];
                var dao = new USERDAO();
                if (!string.IsNullOrEmpty(user.Passwords))
                {
                    var encrytedMd5 = Encryptor.MD5Hash(user.Passwords);
                    user.Passwords = encrytedMd5;
                    user.ModifiedBy = session.TenTK;
                    user.ModifiedDate = DateTime.Now;
                    user.Position = session.ChucVu;
                }
                var result = dao.UpdateDetail(user);
                if (result)
                {
                    SetAlert("Chỉnh sửa thành công", "success");
                }
                else
                {
                    SetAlert("Cập nhật tài khoản không thành công", "error");
                }

            }
            return View(user);

        }

       public ActionResult Logout()
        {
            Session[CommomConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login/Index");
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        
         [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = new USERDAO().ViewDetail(id);     
            return View(user);
        }
        [HttpPost]       
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModels user)
        {            
            if (ModelState.IsValid)
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];
                var dao = new USERDAO();
                if (dao.CheckUserName(user.UserName))
                {
                    SetAlert("Tên đăng nhập đã có người đăng kí", "error");
                }
                else if (dao.CheckUserEmail(user.Email))
                {
                    SetAlert("Tài khoản Email đã tồn tại", "error");
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Passwords);
                    user.Passwords = encryptedMd5Pas;
                    user.CreatedDate = DateTime.Now;
                    user.CreatedBy = "Admin";
                    user.ModifiedBy = session.TenTK;
                   
                    bool id = dao.Insert(user);
                    if (id ==true)
                    {
                        SetAlert("Thêm User thành công", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAlert("Thêm User không thành công", "error");
                    }
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserModels user, string id)
        {
            try
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];
                var dao = new USERDAO();
                if (!string.IsNullOrEmpty(user.Passwords))
                {
                    var encrytedMd5 = Encryptor.MD5Hash(user.Passwords);
                    user.Passwords = encrytedMd5;
                    user.ModifiedBy = session.TenTK;
                    user.ModifiedDate = DateTime.Now;
                }
            
                var result = dao.Update(user, id);
                if (result)
                {
                    SetAlert("Chỉnh sửa thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật tài khoản không thành công", "error");
                        return View();
                }
            
            }
            catch
            {
                return View();
            }
         
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var oders = new OrderDao();
            bool result = oders.hasOrderDetail(id);
            if(result==false)
            {
                SetAlert("Xoá thất bại !!!", "error");
                
            }
            else
            {
                new USERDAO().Delete(id);
                SetAlert("Xóa thành công", "success");
            }         
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public ActionResult ChangePassword(Changepass model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserSession)Session[CommomConstants.USER_SESSION];
                var dao = new USERDAO();
             
                    var result = dao.ChangePass(session.UserID, Encryptor.MD5Hash(model.newPassswords));
                    if (result == 2 )
                    {
                        SetAlert("Đổi mật khẩu thành công", "success");
                        return RedirectToAction("Index","Default");
                    }
                    else if(result==1)
                    {                     
                        ModelState.AddModelError("", " Bạn phải nhập mật khẩu khác");
                    }                                         
           }
            return View(model);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = new USERDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public ActionResult DashBoard()
        {
            var dao = new USERDAO();
            var list = dao.GetAll();
            List<int> reparttitons = new List<int>();
            var posion = list.Select(x => x.Position).Distinct();
            foreach(var item in posion)
            {
                reparttitons.Add(list.Count(x => x.Position == item));
            }

            var rep = reparttitons;
            ViewBag.POSI = posion ;
            ViewBag.REP = reparttitons.ToList();
            return View();
        }
        
    }
}