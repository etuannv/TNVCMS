using System.Collections.Generic;
using TNVCMS.Utilities;
using TNVCMS.Domain.Model;

namespace TNVCMS.Domain.Services
{
    public interface IT_ImageServices
    {
        IEnumerable<T_Image> GetAll();
        T_Image GetByID(int id);
        ReturnValue<bool> AddNewImage(T_Image iImage);
        T_Image AddNewImageAndReturn(T_Image iImage);
        ReturnValue<bool> UpdateImage(T_Image iImage);
        ReturnValue<bool> DeleteImage(T_Image iImage);
        ReturnValue<bool> DeleteImage(int id);
        IEnumerable<T_Image> ImageSearch(string term);
        IEnumerable<T_Image> GetImageByAlbumID(int GroupID);
    }
}
