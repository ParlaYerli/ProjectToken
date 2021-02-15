using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Entities;

namespace Web.API.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectTokenDBContext _context;

        public UnitOfWork(ProjectTokenDBContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
