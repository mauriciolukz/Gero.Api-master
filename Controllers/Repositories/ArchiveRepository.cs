using System;
using Gero.API.Enumerations;
using Gero.API.Controllers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gero.API.Controllers.Repositories
{
    public class ArchiveRepository<T> : IArchive<T> where T : class
    {
        private readonly DbContext _context;

        public ArchiveRepository(DbContext context)
        {
            _context = context;
        }

        public T Archive(Guid id)
        {
            var model = _context.Set<T>().FindAsync(id);
            
            //if (model != null)
            //{
            //    model.Status = Enums.Status.Inactive;
            //    model.UpdatedAt = DateTimeOffset.Now;

            //    _context.Entry(model).State = EntityState.Modified;
            //}

            return null;
        }
    }
}
