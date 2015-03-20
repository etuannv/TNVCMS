using TNVCMS.Data.DatabaseModel;
using System.Collections.Generic;

namespace TNVCMS.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<T_News> T_Newses { get; set; }
    }
}