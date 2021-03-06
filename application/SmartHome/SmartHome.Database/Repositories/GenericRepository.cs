using Microsoft.EntityFrameworkCore;
using SmartHome.Database;
using SmartHome.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Database.Repositories
{
    public class GenericRepository<T> : IDisposable
        where T : BaseModel
    {
        private readonly DatabaseContext _databaseContext;

        public GenericRepository()
        {
            _databaseContext = new DatabaseContext();
        }

        public T Add(T data)
        {
            data.Id = Guid.NewGuid();
            data.CreatedOn = DateTime.UtcNow;
            data.UpdatedOn = DateTime.UtcNow;
            _databaseContext.Set<T>().Add(data);
            _databaseContext.SaveChanges();
            return data;
        }

        public void Delete(Guid Id)
        {
            var model = _databaseContext.Set<T>().Single(x => x.Id == Id);
            _databaseContext.Set<T>().Remove(model);
            _databaseContext.SaveChanges();
        }

        public T Get(Guid Id)
        {
            return _databaseContext.Set<T>().Single(x => x.Id.ToString() == Id.ToString());
        }

        public List<T> GetAll()
        {
            return _databaseContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetEnumerable()
        {
            return _databaseContext.Set<T>();
        }
        
        public DbSet<T> GetDbSet()
        {
            return _databaseContext.Set<T>();
        }

        public T Update(T data)
        {
            data.UpdatedOn = DateTime.UtcNow;
            _databaseContext.Set<T>().Attach(data);
            _databaseContext.Entry(data).State = EntityState.Modified;
            _databaseContext.SaveChanges();
            return data;
        }
        
        public void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }
        public void Dispose()
        {
            _databaseContext.Dispose();
        }
    }
}
