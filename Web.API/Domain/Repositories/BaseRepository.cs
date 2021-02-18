using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Entities;

namespace Web.API.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly TokenContext _context;

        public BaseRepository(TokenContext context)
        {
            _context = context;
        }
    }
}
