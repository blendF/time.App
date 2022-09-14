using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ILanguageRepository Language { get; }

        void Save();
    }
}
