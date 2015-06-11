
using System.Collections.Generic;
using TNVCMS.Domain.Model;

namespace TNVCMS.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<T_News> T_Newses { get; set; }
    }
}