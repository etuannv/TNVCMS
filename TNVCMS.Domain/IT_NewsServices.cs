using System.Collections.Generic;
using TNVCMS.Domain.Model;
using TNVCMS.Utilities;


namespace TNVCMS.Domain.Services
{
    public interface IT_NewsServices
    {
        IEnumerable<T_News> GetAll();

        IEnumerable<T_News> GetNews(int? cateId, string search);
        IEnumerable<T_News> GetByCategory(int iCateID);
        T_News GetByID(int id);
        T_News GetBySlug(string slug);
        ReturnValue<bool> AddNewNews(T_News iNews);
        T_News AddNewNewsAndReturn(T_News iNews);
        ReturnValue<bool> UpdateNews(T_News iNews);
        ReturnValue<bool> DeleteNews(T_News iNews);
        ReturnValue<bool> DeleteNews(int id);
    }
}
