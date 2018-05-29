using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Framework.Platform.Core;

namespace Web.Models
{

    public class JDatatableResult
    {
        public JDatatableResult()
        {

        }
        public int draw { get; set; }
        // recordsTotal：所有的总件数
        public int recordsTotal { get; set; }
        // recordsFiltered：筛选后的总件数
        public int recordsFiltered { get; set; }
        // data：返回的数据
        public object data { get; set; }

    }
}
