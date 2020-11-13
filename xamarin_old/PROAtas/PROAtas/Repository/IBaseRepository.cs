using PROAtas.Model.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROAtas.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> Find(Func<TEntity, bool> predicate);
        int Add(TEntity entity);
        int AddRange(List<TEntity> entities);
        int Update(TEntity entity);
        int UpdateRange(List<TEntity> entities);
        int Remove(TEntity entity);
    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly SQLiteConnection database;
        protected Dictionary<int, TEntity> dictionary = new Dictionary<int, TEntity>();

        public BaseRepository(SQLiteConnection context)
        {
            database = context;
        }

        public TEntity Get(int id)
        {
            if (dictionary.ContainsKey(id))
                return dictionary[id];

            return this.database.Find<TEntity>(id);
        }

        public List<TEntity> GetAll()
        {
            var entities = this.database.Table<TEntity>().ToList();
            entities.ForEach(l =>
            {
                if (!dictionary.ContainsKey(l.Id))
                    dictionary.Add(l.Id, l);
                else
                    dictionary[l.Id] = l;
            });

            return entities;
        }

        public List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var entities = this.database.Table<TEntity>().Where(predicate).ToList();
            entities.ForEach(l =>
            {
                if (!dictionary.ContainsKey(l.Id))
                    dictionary.Add(l.Id, l);
                else
                    dictionary[l.Id] = l;
            });

            return entities;
        }

        public int Add(TEntity entity)
        {
            dictionary.Add(entity.Id, entity);

            return this.database.Insert(entity);
        }

        public int AddRange(List<TEntity> entities)
        {
            entities.ForEach(l => dictionary.Add(l.Id, l));

            return this.database.InsertAll(entities);
        }

        public int Update(TEntity entity)
        {
            if (!dictionary.ContainsKey(entity.Id))
                dictionary.Add(entity.Id, entity);
            else
                dictionary[entity.Id] = entity;

            return this.database.Update(entity);
        }

        public int UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(l =>
            {
                if (!dictionary.ContainsKey(l.Id))
                    dictionary.Add(l.Id, l);
                else
                    dictionary[l.Id] = l;
            });

            return this.database.UpdateAll(entities);
        }

        public int Remove(TEntity entity)
        {
            dictionary.Remove(entity.Id);

            return this.database.Delete(entity);
        }
    }
}
