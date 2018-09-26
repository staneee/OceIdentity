using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDS4Service.Validator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            if (context.UserName == "admin" && context.Password == "123")
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: "custom",
                    claims: new Claim[]
                    {
                        new Claim("Name", context.UserName),

                        new Claim("UserId", "1"),
                        new Claim("RealName", "玩双截棍的熊猫"),
                        new Claim("Email", "msmadaoe@msn.com")
                    });
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
        }
    }
}
