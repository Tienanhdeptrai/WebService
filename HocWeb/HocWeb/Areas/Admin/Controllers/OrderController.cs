﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocWeb.Areas.Admin.Code;
using HocWeb.DAO;
using HocWeb.Models;

namespace HocWeb.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        [HttpGet]
        public ActionResult Detail(string id)
        {
            var result = new OrderDao().ViewDetail(id);
            var dao = new USERDAO().ViewDetail(result.CustomerID.ToString());
            IList<OrderDetailModels> product = new OrderDao().GetAll(id);
            IList<UserModels> user = new List<UserModels>();
            user.Add(dao);
            ViewData["KHACHHANG"] = user;
            ViewData["SANPHAM"] = product;
            return View(result);
        }
        public ActionResult Index()
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging();
            return View(model);
        }
       
        [HttpPost]
        public ActionResult Detail(OrderModels orders,string id)
        {
            if (ModelState.IsValid)
            {
                var result = new OrderDao().ViewDetail(id);
                var dao = new USERDAO().ViewDetail(result.CustomerID.ToString());
                IList<OrderDetailModels> product = new OrderDao().GetAll(id);
                IList<UserModels> user = new List<UserModels>();
                user.Add(dao);
                ViewData["KHACHHANG"] = user;
                ViewData["SANPHAM"] = product;
                var order = new OrderDao();
                order.update(orders,id);
                SetAlert("Chỉnh sửa trạng thái thành công", "success");
                RedirectToAction("index");
             
            }           
            return View(orders);
        }
    }
}