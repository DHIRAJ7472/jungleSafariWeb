using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApi.Infrastructure
{
    public class MyJwtToken
    {
        public string Token { set; get; }
        public string RoleName { set; get; }
    }
}