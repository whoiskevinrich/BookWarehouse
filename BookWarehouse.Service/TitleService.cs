using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Service
{
    public class TitleService : ITitleService
    {
        private readonly IRepository<Title> _titles;

        public TitleService(IRepository<Title> titles)
        {
            _titles = titles;
        }

        public void Create(Title title)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            _titles.Add(title);
        }

        public void Delete(Guid id)
        {
            _titles.Remove(id);
        }

        public Title Find(Expression<Func<Title, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _titles.SearchFor(predicate);
        }

        public Title Get(Guid id)
        {
            return _titles.Find(id);
        }

        public IEnumerable<Title> GetAll()
        {
            return _titles.GetAll();
        }

        public void Update(Title title)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            _titles.Update(title);
        }

        public IQueryable<Title> Where(Expression<Func<Title, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _titles.SearchForMany(predicate);
        }
    }
}