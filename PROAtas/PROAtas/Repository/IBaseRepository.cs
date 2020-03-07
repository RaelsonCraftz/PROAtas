using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROAtas.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        int Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);
        int Update(TEntity entity);
        int UpdateRange(IEnumerable<TEntity> entities);
        int Remove(TEntity entity);
    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly SQLiteConnection database;

        public BaseRepository(SQLiteConnection context)
        {
            database = context;
        }

        public TEntity Get(int id)
        {
            return this.database.Find<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.database.Table<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return this.database.Table<TEntity>().ToList().Where(predicate).ToList();
        }

        public int Add(TEntity entity)
        {
            return this.database.Insert(entity);
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            return this.database.InsertAll(entities);
        }

        public int Update(TEntity entity)
        {
            return this.database.Update(entity);
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            return this.database.UpdateAll(entities);
        }

        public int Remove(TEntity entity)
        {
            return this.database.Delete(entity);
        }
    }
}
