using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Platform.Core
{
    public class DomainException : Exception
    {
        public int ErroCode { get; set; }
        public DomainException(string message,int ErroCode,Exception ex) : base(message, ex)
        {
            this.ErroCode = ErroCode;

        }
        public DomainException(string message, int ErroCode) : base(message)
        {
            this.ErroCode = ErroCode;

        }
    }
}
