using System;
using System.Data.Entity.Core.Objects;

namespace TNVCMS.Data.DataAcess
{
    public interface IObjectContext : IDisposable
    {
        void SaveChanges();
        ObjectContextOptions ContextOptions { get; }
    }
}
