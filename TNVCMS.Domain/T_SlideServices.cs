using System;
using System.Collections.Generic;
using System.Linq;
using TNVCMS.Domain.Model;
using TNVCMS.Utilities;

namespace TNVCMS.Domain.Services
{
    public class T_SlideServices : IT_SlideServices
    {
        private TNVCMSEntities _dataContext;

        public T_SlideServices()
        {
            _dataContext = new TNVCMSEntities();
        }

        public IEnumerable<T_Slide> GetAll()
        {
            return from m in _dataContext.T_Slide
                    select m;
        }

        public T_Slide GetByID(int id)
        {
            return _dataContext.T_Slide.Where(m => m.ID == id).SingleOrDefault();
        }

        public bool IsExist(T_Slide iSlide)
        {
            return false;
        }

        public T_Slide AddNewSlideAndReturn(T_Slide iSlide)
        {
            _dataContext.T_Slide.Add(iSlide);
                _dataContext.SaveChanges();
                return iSlide;
        }


        public ReturnValue<bool> AddNewSlide(T_Slide iSlide)
        {
            if (IsExist(iSlide)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                _dataContext.T_Slide.Add(iSlide);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> UpdateSlide(T_Slide iSlide)
        {
            //if (IsExist(iSlide)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                T_Slide UpdatedItem = _dataContext.T_Slide.Where(m => m.ID == iSlide.ID).SingleOrDefault();
                UpdatedItem.Title = iSlide.Title;
                UpdatedItem.Link = iSlide.Link;
                UpdatedItem.ImagePath = iSlide.ImagePath;
                UpdatedItem.Enable = iSlide.Enable;
                return new ReturnValue<bool>(_dataContext.SaveChanges() > 0, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public ReturnValue<bool> DeleteSlide(T_Slide iSlide)
        {
            try
            {
                _dataContext.T_Slide.Remove(iSlide);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> DeleteSlide(int id)
        {
            try
            {
                T_Slide DelSlide = GetByID(id);
                return DeleteSlide(DelSlide);
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public IEnumerable<T_Slide> SlideSearch(string term)
        {
            IEnumerable<T_Slide> ResultList;
            if (!string.IsNullOrEmpty(term))
            {
                string SlugTerm = term.Replace(' ', '-');
                ResultList = _dataContext.T_Slide.Where(m => m.Title.Contains(term));
            }
            else
            {
                ResultList = GetAll();
            }
            return ResultList.OrderByDescending(m => m.ID);
        }
        public IEnumerable<T_Slide> GetEnableSlide()
        {
            return _dataContext.T_Slide.Where(m => m.Enable == true);
        }
    }
}
