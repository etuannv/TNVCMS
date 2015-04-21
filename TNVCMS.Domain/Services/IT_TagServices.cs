using System.Collections.Generic;
using TNVCMS.Data.DatabaseModel;
using TNVCMS.Utilities;


namespace TNVCMS.Domain.Services
{
    public interface IT_TagServices
    {
        IEnumerable<T_Tag> GetAll();
        IEnumerable<T_Tag> GetByTaxonomy(string taxonomy);
        IEnumerable<T_Tag> GetByTaxonomy(string taxonomy, string searchKey);
        T_Tag GetByID(int id);
        ReturnValue<bool> AddNewTag(T_Tag iTag);
        ReturnValue<bool> UpdateTag(T_Tag iTag);
        ReturnValue<bool> DeleteTag(T_Tag iTag);
        ReturnValue<bool> DeleteTag(int id);
    }
}
