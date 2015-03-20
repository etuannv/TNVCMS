using System;

namespace TNVCMS.Data.DataAcess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        bool LazyLoadingEnabled { set; get; }
    }
}
