using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PYS.IdentityServer.Security.Administration.Authorize
{
    public class Pagination 
    {
        [Display (Name = "Páginas")]
        public int Pages { get; set; }
        [Display (Name = "Página Actual")]
        public int Page { get; set; }
        [Display (Name = "Items por página")]
        public int ItemsByPage { get; set; }
    }
}
