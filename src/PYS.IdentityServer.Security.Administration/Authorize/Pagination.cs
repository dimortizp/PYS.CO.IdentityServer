using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.Authorize
{
    public class Pagination 
    {
        public int Pages { get; set; }
        public int Page { get; set; }
        public int ItemsByPage { get; set; }
    }
}
