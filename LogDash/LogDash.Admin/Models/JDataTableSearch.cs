using Framework.Platform.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public enum SortType
    {
        Asc = 1,
        Desc = 2,
        None = 0
    }
    public class JDataTableSearch
    {
        /// <summary>
        /// 和Request的draw设定成一样的值
        /// </summary>
        public int draw { get; set; }


        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortColumnName { get { return _orderColumn; } }
        /// <summary>
        /// 升降序
        /// </summary>
        public SortType Sort
        {
            get
            {
                if (_orderDir == "desc")
                {
                    return SortType.Desc;
                }
                if (_orderDir == "asc")
                {
                    return SortType.Asc;
                }
                return SortType.None;
            }
        }


        // start:开始记录（0 besed index）
        public int start { get; set; }
        // length:每页条数
        public int length { get; set; }
        /// <summary>
        /// 检索文字
        /// </summary>
        public string SearchValue
        {
            get
            {
                try
                {
                    return Convert.ToString(MyHttpContext.Current.Request.Query["search[value]"]);
                }
                catch
                {
                    return "";
                }

            }
        }



        private string _orderColumn
        {
            get
            {
                try
                {
                    var i = Convert.ToString(MyHttpContext.Current.Request.Query["order[0][column]"]);
                    return Convert.ToString(MyHttpContext.Current.Request.Query["columns[" + i + "][data]"]);

                }
                catch
                {
                    return "";
                }

            }
        }
        private string _orderDir
        {
            get
            {
                try
                {
                    return Convert.ToString(MyHttpContext.Current.Request.Query["order[0][dir]"]);
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
