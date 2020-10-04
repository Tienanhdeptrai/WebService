﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocWeb.Areas.Admin.Models;
using HocWeb.Areas.Admin.Code;
using HocWeb.DAO;


namespace HocWeb.Areas.Admin.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Admin/Login       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new USERDAO();
                var result = dao.Login(model.TenTK, Encryptor.MD5Hash(model.MatKhau));
               
                if(result==2)
                {
                    var user = dao.GetByID(model.TenTK);
                    var userSession = new UserSession();
                    userSession.TenTK = user.UserName;
                    userSession.UserID = user.UserID.ToString();
                    userSession.ChucVu = user.Position;
                    userSession.Name = user.FirstName + " " + user.LastName;
                    Session.Add(CommomConstants.USER_SESSION,userSession);

                    if(user.Position=="1")
                    {
                        ModelState.AddModelError("", "Bạn không có quyền đăng nhập trang với tư cách này");
                    }
                    return RedirectToAction("Index", "Default");
                } else if(result ==0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }            
                else if(result==1)
                {
                    var user = dao.GetByID(model.TenTK);
                    var userSession = new UserSession();
                    userSession.TenTK = user.UserName;
                    userSession.UserID = user.UserID.ToString();
                    userSession.ChucVu = user.Position;
                    userSession.Name = user.FirstName + " " + user.LastName;
                    Session.Add(CommomConstants.USER_SESSION, userSession);
                    if (user.Position == "2")
                    {
                        ModelState.AddModelError("", "Bạn không có quyền đăng nhập trang với tư cách này");
                    }                    
                }               
            }
            return View("Index");        
        }     
    }
}