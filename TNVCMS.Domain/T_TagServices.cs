using System;
using System.Collections.Generic;
using System.Linq;
using TNVCMS.Domain.Model;
using TNVCMS.Utilities;

namespace TNVCMS.Domain.Services
{
    public class T_TagServices : IT_TagServices
    {
        private TNVCMSEntities _dataContext;

        public T_TagServices()
        {
            _dataContext = new TNVCMSEntities();
        }

        public IEnumerable<T_Tag> GetAll()
        {
            return from m in _dataContext.T_Tag
                    select m;
        }
        public IEnumerable<T_Tag> GetByTaxonomy(string taxonomy)
        {
            return _dataContext.T_Tag.Where(m => m.Taxonomy == taxonomy).OrderBy(m => m.Title);
        }

        public IEnumerable<T_Tag> GetByTaxonomyForDisplay(string taxonomy, IEnumerable<T_Tag> excepTagList = null, string searchKey = null)
        {
            IEnumerable<T_Tag> TagList;
            if (!string.IsNullOrEmpty(searchKey))
            {
                string searchSlug = searchKey.Replace(' ', '-');
                TagList =
                    _dataContext.T_Tag.Where(
                    m => m.Taxonomy == taxonomy
                   && (m.Title.Contains(searchKey) || m.Slug.Contains(searchSlug)))
                   .OrderBy(m => m.ParentPath);
            }
            else
            {
                TagList = _dataContext.T_Tag.Where(m => m.Taxonomy == taxonomy).OrderBy(m => m.ParentPath);
            }
            // Remove exept list
            if (excepTagList != null)
            {
                TagList = TagList.Except(excepTagList);
            }
            if (taxonomy == Constants.TAXONOMY_CATEGORY)
            {
                // Modify title
                foreach (var item in TagList)
                {
                    string AddString = "";
                    int count = item.ParentPath.Count(f => f == ';');
                    for (int i = 0; i < count; i++)
                    {
                        AddString += "— ";
                    }
                    item.Title = AddString + item.Title;
                }
            }
            return TagList;
        }
        public IEnumerable<T_Tag> GetByTaxonomy(string taxonomy, string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey))
            {
                string searchSlug = searchKey.Replace(' ', '-');
                return _dataContext.T_Tag.Where(
                    m => m.Taxonomy == taxonomy
                    && (m.Title.Contains(searchKey) || m.Slug.Contains(searchKey)))
                    .OrderBy(m => m.Title);
            }
            else
            {
                return _dataContext.T_Tag.Where(m => m.Taxonomy == taxonomy).OrderBy(m => m.Title);
            }
        }
        public T_Tag GetByID(int id)
        {
            return _dataContext.T_Tag.Where(m => m.ID == id).SingleOrDefault();
        }
        public T_Tag GetBySlug(string slug)
        {
            return _dataContext.T_Tag.Where(m => m.Slug == slug).SingleOrDefault();
        }

        public bool IsExist(T_Tag iTag)
        {
            T_Tag TagFound = _dataContext.T_Tag.Where(m => m.Title == iTag.Title || m.Slug == iTag.Slug).SingleOrDefault();
            return (TagFound != null) ? true : false;
        }

        public T_Tag AddNewTagAndReturn(T_Tag iTag)
        {
            //Check exist
            T_Tag TagFound = _dataContext.T_Tag.Where(m => m.Title == iTag.Title || m.Slug == iTag.Slug).SingleOrDefault();
            //Return exist Tag
            if (TagFound != null) return TagFound;
            else
            {
                _dataContext.T_Tag.Add(iTag);
                _dataContext.SaveChanges();
                return iTag;
            }
        }
        
        public T_Tag AddNewTagAndReturn(string iTag)
        {
            iTag = iTag.Trim();
            T_Tag NewTag = new T_Tag();
            NewTag.Title = iTag;
            NewTag.Slug = Utilities.Common.ToUrlSlug(iTag);
            NewTag.Taxonomy = Utilities.Constants.TAXONOMY_TAG;
            return AddNewTagAndReturn(NewTag);
        }

        public ReturnValue<bool> AddNewTag(T_Tag iTag)
        {
            if (IsExist(iTag)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                _dataContext.T_Tag.Add(iTag);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> UpdateTag(T_Tag iTag)
        {
            //if (IsExist(iTag)) return new ReturnValue<bool>(false, "Mục đã tồn tại");
            try
            {
                T_Tag UpdatedItem = _dataContext.T_Tag.Where(m => m.ID == iTag.ID).SingleOrDefault();
                UpdatedItem.Title = iTag.Title;
                UpdatedItem.Description = iTag.Description;
                UpdatedItem.Slug = iTag.Slug;
                UpdatedItem.ParentID = iTag.ParentID;
                UpdatedItem.ParentPath = iTag.ParentPath;
                UpdatedItem.ModifiedBy = iTag.ModifiedBy;
                UpdatedItem.ModifiedDate = iTag.ModifiedDate;
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public ReturnValue<bool> DeleteTag(T_Tag iTag)
        {
            try
            {
                _dataContext.T_Tag.Remove(iTag);
                _dataContext.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> DeleteTag(int id)
        {
            try
            {
                T_Tag DelTag = GetByID(id);
                return DeleteTag(DelTag);
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

        public string GetPath(int? parentID)
        {
            string Result = "";
            if (parentID != null)
            {
                T_Tag Parent = GetByID((int)parentID);
                Result += Parent.ParentPath;
                Result += ";";
            }
            Result += Utilities.Common.GetUniqueString();
            return Result;
        }

        public IEnumerable<T_Tag> TagSearch(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                string SlugTerm = term.Replace(' ', '-');
                return _dataContext.T_Tag.Where(
                    m => m.Taxonomy == Utilities.Constants.TAXONOMY_TAG
                    && (m.Title.StartsWith(term) || m.Slug.StartsWith(SlugTerm))
                    );
            }
            else
            {
                return _dataContext.T_Tag.Where(m => m.Taxonomy == Utilities.Constants.TAXONOMY_TAG);
            }
        }

    }
}
