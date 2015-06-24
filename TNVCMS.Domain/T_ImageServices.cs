using System;
using System.Collections.Generic;
using System.Linq;
using TNVCMS.Domain.Model;
using System.Data.Entity;
using TNVCMS.Utilities;

namespace TNVCMS.Domain.Services
{
    public class T_ImageServices : IT_ImageServices
    {
        private TNVCMSEntities _dataContext;

        public T_ImageServices()
        {
            _dataContext = new TNVCMSEntities();
        }

        public IEnumerable<T_Image> GetAll()
        {
            return from m in _dataContext.T_Image.Include(m=>m.T_Album)
                    select m;
        }

        public T_Image GetByID(int id)
        {
            return _dataContext.T_Image.Where(m => m.ID == id).SingleOrDefault();
        }

        public bool IsExist(T_Image iImage)
        {
            return false;
        }

        public T_Image AddNewImageAndReturn(T_Image iImage)
        {
            _dataContext.T_Image.Add(iImage);
                _dataContext.SaveChanges();
                return iImage;
        }


        public ReturnValue<bool> AddNewImage(T_Image iImage)
        {
            if (IsExist(iImage)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                _dataContext.T_Image.Add(iImage);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> UpdateImage(T_Image iImage)
        {
            //if (IsExist(iImage)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                _dataContext.Entry(iImage).State = EntityState.Modified;
                return new ReturnValue<bool>(_dataContext.SaveChanges() > 0, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public ReturnValue<bool> DeleteImage(T_Image iImage)
        {
            try
            {
                _dataContext.T_Image.Remove(iImage);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> DeleteImage(int id)
        {
            try
            {
                T_Image DelImage = GetByID(id);
                return DeleteImage(DelImage);
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public IEnumerable<T_Image> ImageSearch(string term)
        {
            IEnumerable<T_Image> ResultList;
            if (!string.IsNullOrEmpty(term))
            {
                ResultList = from m in _dataContext.T_Image.Include(m => m.AlbumID)
                             where (m.Title.Contains(term))
                             select m;
            }
            else
            {
                ResultList = GetAll();
            }
            return ResultList.OrderByDescending(m => m.ID);
        }

        public IEnumerable<T_Image> GetImageByAlbumID(int AlbumID)
        {
            return _dataContext.T_Image.Where(m => m.AlbumID == AlbumID);
        }
    }
}
