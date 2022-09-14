using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;

namespace TimeApp.DataAccess.Repository.IRepository
{
    public interface ILanguageRepository : IRepository<Language>
    {
        void Update(Language Language);
    }
}
