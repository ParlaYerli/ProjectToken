using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Entities;

namespace Web.API.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly ProjectTokenDBContext _context;

        public BaseRepository(ProjectTokenDBContext context)
        {
            _context = context;
        }
    }
}
