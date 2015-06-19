﻿using System;
using System.Collections.Generic;
using System.Linq;
using TNVCMS.Domain.Model;
using TNVCMS.Utilities;
using System.Data.Entity;

namespace TNVCMS.Domain.Services
{
    public class T_NewsServices : IT_NewsServices
    {
        private TNVCMSEntities _dataContext;

        public T_NewsServices()
        {
            _dataContext = new TNVCMSEntities();
        }

        public IEnumerable<T_News> GetAll()
        {
            return from m in _dataContext.T_News
                   select m;
        }

        public IEnumerable<T_News> GetNews(int? cateId, string search)
        {
            IEnumerable<T_News> ResultList = GetAll();
            if (cateId != null) ResultList = ResultList.Where(m => m.T_News_Tag.Any(b => b.TagID == cateId));
            if (!string.IsNullOrEmpty(search))
            {
                string SearchSlug = search.Replace(' ', '-');
                ResultList = ResultList.Where(m => m.Title.Contains(search) || m.Slug.Contains(SearchSlug));
            }
            return ResultList.OrderByDescending(k => k.ID);
        }
        public IEnumerable<T_News> GetByTaxonomy(int iCateID)
        {
            var data = from n in _dataContext.T_News
                    join m in _dataContext.T_News_Tag on n.ID equals m.NewsID
                    join q in _dataContext.T_Tag on m.TagID equals q.ID
                    where m.TagID == iCateID
                    select n;
            
            return data.OrderByDescending(a=>a.ID);
        }

        public IEnumerable<T_News> GetByTaxonomy(int iCateID, int number)
        {

            return GetByTaxonomy(iCateID).OrderByDescending(m => m.ID).Select(m => m).Take(number);
        }


        public T_News GetByID(int id)
        {
            return _dataContext.T_News.Where(m => m.ID == id).SingleOrDefault();
        }
        public T_News GetBySlug(string slug)
        {
            return _dataContext.T_News.Where(m => m.Slug == slug).SingleOrDefault();
        }

        public bool IsExist(T_News iNews)
        {
            T_News NewsFound = _dataContext.T_News
                .Where(m => m.ID != iNews.ID && (m.Title == iNews.Title || m.Slug == iNews.Slug))
                .SingleOrDefault();
            return (NewsFound != null) ? true : false;
        }

        public T_News AddNewNewsAndReturn(T_News iNews)
        {
            _dataContext.T_News.Add(iNews);
            _dataContext.SaveChanges();
            return iNews;
        }
        public ReturnValue<bool> AddNewNews(T_News iNews)
        {
            if (IsExist(iNews)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                _dataContext.T_News.Add(iNews);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> UpdateNews(T_News iNews)
        {
            if (IsExist(iNews)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                T_News UpdateItem = GetByID(iNews.ID);
                UpdateItem.Title = iNews.Title;
                UpdateItem.Slug = iNews.Slug;
                UpdateItem.AvataImageUrl = iNews.AvataImageUrl;
                UpdateItem.IsHotNews = iNews.IsHotNews;
                UpdateItem.ContentNews = iNews.ContentNews;
                UpdateItem.Author = iNews.Author;
                UpdateItem.PublishTime = iNews.PublishTime;
                UpdateItem.Status = iNews.Status;
                UpdateItem.ModifiedBy = iNews.ModifiedBy;
                UpdateItem.ModifiedDate = iNews.ModifiedDate;
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public ReturnValue<bool> DeleteNews(T_News iNews)
        {
            try
            {
                _dataContext.T_News.Remove(iNews);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> DeleteNews(int id)
        {
            try
            {
                T_News DelNews = GetByID(id);
                return DeleteNews(DelNews);
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public IEnumerable<T_News> GetLastNews(int limit)
        {
            return _dataContext.T_News.OrderByDescending(m => m.ID).Select(m => m).Take(limit);
        }

        public IEnumerable<T_News> GetRelatedNews(int newsId, int limit)
        {
            //Search news for same tag
            IT_TagServices tagService = new T_TagServices();
            List<T_Tag> TagList = tagService.GetTagByNewsID(TNVCMS.Utilities.Constants.TAXONOMY_TAG, newsId).ToList(); ;
            return GetNewsByTagList(TagList, limit);
        }

        private IEnumerable<T_News> GetNewsByTagList(List<T_Tag> TagList, int limit)
        {
            List<int> ListTagID = TagList.Select(s => s.ID).ToList();
            return (from m in _dataContext.T_News
                    join n in _dataContext.T_News_Tag on m.ID equals n.NewsID
                    where ListTagID.Contains(n.TagID)
                    select m).Take(limit).Distinct();
        }

        public IEnumerable<T_News> GetNewsByTag(int tagId, int limit)
        {
            var q = (from m in _dataContext.T_News
                    join n in _dataContext.T_News_Tag on m.ID equals n.NewsID
                    where n.TagID == tagId
                    select m).Distinct().OrderByDescending(s=>s.ID);
            if(limit > 0)
                return q.Take(limit);
            else
                return q;
        }

        public T_Tag GetCateByNewsID(int newsID)
        {
            return (from m in _dataContext.T_Tag
                     join n in _dataContext.T_News_Tag on m.ID equals n.TagID
                     where n.NewsID == newsID && m.Taxonomy == TNVCMS.Utilities.Constants.TAXONOMY_CATEGORY
                     select m).FirstOrDefault();
        }
    }
}
