using System.Collections.Generic;
using System.Linq;
using TNVCMS.Data.DataAcess;
using TNVCMS.Data.DatabaseModel;

namespace TNVCMS.Domain.Services
{
    public class T_NewsServices : IT_NewsServices
    {
        private readonly IRepository<T_News> _newsRepository;
        
        public T_NewsServices(IRepository<T_News> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IEnumerable<T_News> GetAllNews()
        {
            return _newsRepository.GetAll();
            
        }

    }
}
