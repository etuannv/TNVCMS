using System;
using System.Collections.Generic;
using System.Linq;
using TNVCMS.Data.DataAcess;
using TNVCMS.Data.DatabaseModel;
using TNVCMS.Utilities;

namespace TNVCMS.Domain.Services
{
    public class T_TagServices : IT_TagServices
    {
        private readonly IRepository<T_Tag> _tagRepository;

        public T_TagServices(IRepository<T_Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<T_Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }
        public IEnumerable<T_Tag> GetByTaxonomy(string taxonomy)
        {
            return _tagRepository.Find(m => m.Taxonomy == taxonomy).OrderBy(m => m.Title);
        }
        public IEnumerable<T_Tag> GetByTaxonomy(string taxonomy, string searchKey)
        {
            if(!string.IsNullOrEmpty(searchKey))
                return _tagRepository.Find(m => m.Taxonomy == taxonomy && m.Title.Contains(searchKey)).OrderBy(m => m.Title);
            else
                return _tagRepository.Find(m => m.Taxonomy == taxonomy).OrderBy(m => m.Title);
        }
        public T_Tag GetByID(int id)
        {
            return _tagRepository.Find(m => m.ID == id).SingleOrDefault();
        }

        public ReturnValue<bool> AddNewTag(T_Tag iTag)
        {
            try
            {
                _tagRepository.Insert(iTag);
                _tagRepository.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }
        public ReturnValue<bool> UpdateTag(T_Tag iTag)
        {
            try
            {
                _tagRepository.Update(iTag);
                _tagRepository.SaveChanges();
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
                _tagRepository.Delete(iTag);
                _tagRepository.SaveChanges();
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
                _tagRepository.Delete(DelTag);
                _tagRepository.SaveChanges();
                return new ReturnValue<bool>(true, "");
            }
            catch (Exception)
            {
                return new ReturnValue<bool>(false, "");
            }
        }

    }
}
