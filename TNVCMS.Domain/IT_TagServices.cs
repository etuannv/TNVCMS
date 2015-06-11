using System.Collections.Generic;
using TNVCMS.Utilities;
using TNVCMS.Domain.Model;


namespace TNVCMS.Domain.Services
{
    public interface IT_TagServices
    {
        IEnumerable<T_Tag> GetAll();
        IEnumerable<T_Tag> GetByTaxonomy(string taxonomy);
        IEnumerable<T_Tag> GetByTaxonomyForDisplay(string taxonomy, IEnumerable<T_Tag> excepTagList = null, string searchKey = null);
        IEnumerable<T_Tag> GetByTaxonomy(string taxonomy, string searchKey);
        T_Tag GetByID(int id);
        T_Tag GetBySlug(string slug);
        ReturnValue<bool> AddNewTag(T_Tag iTag);
        
        T_Tag AddNewTagAndReturn(T_Tag iTag);

        T_Tag AddNewTagAndReturn(string iTag);
        ReturnValue<bool> UpdateTag(T_Tag iTag);
        ReturnValue<bool> DeleteTag(T_Tag iTag);
        ReturnValue<bool> DeleteTag(int id);
        string GetPath(int? parentID);

        IEnumerable<T_Tag> TagSearch(string term);

    }
}
