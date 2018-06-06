using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace LogDash.Admin.Controllers
{
    public class AppInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(JDataTableSearch model, string AppId, string AppName)
        {

           IList<AppInfo>  res = LogBusiness.GetAppInfoList() ;

            JDatatableResult jDatatable = new JDatatableResult()
            {
                data = res,
                recordsTotal = res.Count,
                recordsFiltered = res.Count
            };
            return new JsonResult(jDatatable);
        }
    }
}