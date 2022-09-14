using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Data;
using TimeApp.Models;

namespace TimeApp.DataAccess.Repository.IRepository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly ApplicationDbContext _db;

        public LanguageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Language language)
        {
            _db.Update(language);
        }
    }
}
