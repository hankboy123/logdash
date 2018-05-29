using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogDash.Admin.Models;
using Web.Models;
using Web.Validate;
using Web.Common;
using FluentValidation;

namespace LogDash.Admin.Controllers
{
    public class HomeController : BaseController
    {
        #region  base
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateWithTable()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            var res = new aa { id = 1, GamePopularity = 1, IsHotPush = 1, SmallIconUrl = "test", username = "test" };
            return View(res);
        }
        [HttpGet]
        public IActionResult Detial()
        {
            var res = new aa { id = 1, GamePopularity = 1, IsHotPush = 1, SmallIconUrl = "test", username = "test" };
            return View(res);
        }
        [HttpPost]
        public IActionResult Create(aa model)
        {
            //验证
            aaVaildator validator = new aaVaildator();
            //验证不过抛异常
            validator.ValidateAndThrow(model);
            return Success("创建成功！");
        }
        [HttpPost]
        public IActionResult Update(aa model)
        {
            //验证
            aaVaildator validator = new aaVaildator();
            //验证不过抛异常
            validator.ValidateAndThrow(model);
            return Success("更新成功！");
        }


        /// <summary>
        /// jqueryDatatable 查询
        /// </summary>
        /// <param name="model">datatable组件自带参数</param>
        /// <param name="gameName">页面的游戏搜索框</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Search(JDataTableSearch model, string gameName, string selectId)
        {
            System.Threading.Thread.Sleep(1000);
            int count = 10001;
            int PageCount = count % model.length == 0 ? count / model.length : count / model.length + 1;
            int CurrentPage = model.start % model.length == 0 ? model.start / model.length : model.start / model.length + 1;
            int length = model.length * (CurrentPage + 1) > count ? count - model.length * (CurrentPage) : model.length;
            List<aa> list = new List<aa>();
            for (int i = 0; i < length; i++)
            {
                var temp = model.start + i + 1;
                list.Add(new aa { id = temp, GamePopularity = 1, IsHotPush = 1, SmallIconUrl = "test" + temp, username = "test" + temp });
            }
            JDatatableResult jDatatable = new JDatatableResult()
            {
                data = list,
                recordsTotal = count,
                recordsFiltered = count
            };
            return new JsonResult(jDatatable);
        }

        [HttpPost]
        public IActionResult Delete(String[] chk_value)
        {
            return Success("删除成功");
        }
    }
}
