using System.Collections.Generic;
using TNVCMS.Data.DatabaseModel;


namespace TNVCMS.Domain.Services
{
    public interface IT_NewsServices
    {
        IEnumerable<T_News> GetAllNews();
    }
}
