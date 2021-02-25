# Friends in AAD

## Introduction

In this exercise, you have to extend the example regarding *OpenID Connect* and *AAD*. Your job is to build an application for maintaining a directory of friends.

```cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AadAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == ClaimConstants.ObjectId);

            return Ok(new[] { "Order 1", "Order 2" });
        }
    }
}
```
