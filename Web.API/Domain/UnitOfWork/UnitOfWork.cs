using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Entities;

namespace Web.API.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TokenContext _context;

        public UnitOfWork(TokenContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
