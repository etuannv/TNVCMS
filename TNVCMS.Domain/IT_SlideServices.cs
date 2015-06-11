using System.Collections.Generic;
using TNVCMS.Utilities;
using TNVCMS.Domain.Model;

namespace TNVCMS.Domain.Services
{
    public interface IT_SlideServices
    {
        IEnumerable<T_Slide> GetAll();
        T_Slide GetByID(int id);
        ReturnValue<bool> AddNewSlide(T_Slide iSlide);
        T_Slide AddNewSlideAndReturn(T_Slide iSlide);
        ReturnValue<bool> UpdateSlide(T_Slide iSlide);
        ReturnValue<bool> DeleteSlide(T_Slide iSlide);
        ReturnValue<bool> DeleteSlide(int id);
        IEnumerable<T_Slide> SlideSearch(string term);
        IEnumerable<T_Slide> GetEnableSlide();
    }
}
