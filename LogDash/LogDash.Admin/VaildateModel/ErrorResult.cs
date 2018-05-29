using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Platform.Core.VaildateModel
{
    public class ErrorResult
    {
        public ErrorResult() {
            Errors = new List<string>();

        }
        public List<string> Errors { get; set; }
    }
}
