using Application.interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicactionDbContext dbContext;

        public MyRepositoryAsync(ApplicactionDbContext dbContext) : base(dbContext)
        { 
            this.dbContext = dbContext;
        }
    }
}
