using System.Collections.Generic;
using TNVCMS.Utilities;
using TNVCMS.Domain.Model;

namespace TNVCMS.Domain.Services
{
    public interface IT_SlideGroupServices
    {
        IEnumerable<T_SlideGroup> GetAll();
        T_SlideGroup GetByID(int id);
        ReturnValue<bool> AddNewSlideGroup(T_SlideGroup iSlideGroup);
        T_SlideGroup AddNewSlideGroupAndReturn(T_SlideGroup iSlideGroup);
        ReturnValue<bool> UpdateSlideGroup(T_SlideGroup iSlideGroup);
        ReturnValue<bool> DeleteSlideGroup(T_SlideGroup iSlideGroup);
        ReturnValue<bool> DeleteSlideGroup(int id);
        IEnumerable<T_SlideGroup> SlideGroupSearch(string term);
    }
}
