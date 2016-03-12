using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Service
{
    public interface ITitleService
    {
        void Create(Title title);
        void Delete(Guid id);
        Title Find(Expression<Func<Title, bool>> predicate);
        Title Get(Guid id);
        IEnumerable<Title> GetAll();
        void Update(Title title);
        IQueryable<Title> Where(Expression<Func<Title, bool>> predicate);
    }
}