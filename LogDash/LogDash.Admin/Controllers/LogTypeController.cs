using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace LogDash.Admin.Controllers
{
    public class LogTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(JDataTableSearch model, string appId, string logTypeId)
        {

            IList<LogType> res = LogBusiness.GetLogTypeList(appId, logTypeId);

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