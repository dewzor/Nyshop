﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webshop.Data;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Data.DbPopulate.AddDataAsync();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }










        //GET: REDIRECTEN
        //public async Task<ActionResult> Index()
        //{
        //    await DbPopulate.AddDataAsync();
        //    return Redirect("Store/Index");
        //}
    }
}