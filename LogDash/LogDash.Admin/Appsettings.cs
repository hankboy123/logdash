using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Platform.Core
{
    public class Appsettings
    {
        #region private prop

        private string mysqlconnection { get; set; }

        #endregion
        
        public string MySqlConnection
        {
            get { return mysqlconnection; }
            set
            {
                string envMySqlConnection = Environment.GetEnvironmentVariable(nameof(MySqlConnection));//获取环境变量
                if (!string.IsNullOrEmpty(envMySqlConnection))
                    mysqlconnection = envMySqlConnection;
                else
                    mysqlconnection = value;
            }
        }



    }
}
