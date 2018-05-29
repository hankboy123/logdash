using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Validate
{
    public class aaVaildator : AbstractValidator<aa>
    {
        public aaVaildator()
        {
            RuleFor(cmd => cmd.IsHotPush).NotEmpty().WithMessage(r => { return nameof(aa.IsHotPush) + "不能为空"; });
            RuleFor(cmd => cmd.username).NotEmpty().WithMessage(r => { return nameof(aa.username) + "不能为空"; });
            RuleFor(cmd => cmd.SmallIconUrl).NotEmpty().WithMessage(r => { return nameof(aa.SmallIconUrl) + "不能为空"; });
        }
    }
}
