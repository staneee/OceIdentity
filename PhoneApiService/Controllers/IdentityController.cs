using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // 登陆的用户信息
            var resList = new List<string>();

            //resList.Add($"Name : {this.User.Identity.Name}");// 读取的就是"Name"这个特殊的 Claims 的值

            // 所有的 Claims
            resList.AddRange(this.User.Claims.Select(o =>
            {
                return $"{o.Type} : {o.Value}";
            }));

            return resList;
        }
    }
}
