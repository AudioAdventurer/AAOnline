using LiteDB;
using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Data.Interfaces;

namespace AudioAdventurer.Library.Data.Repos
{
    public abstract class AbstractRepo<T> 
        : IRepo<T> 
        where T: IIdentifiableObject
    {
        protected readonly ILiteCollection<T> Collection;

        public abstract string CollectionName { get; }

        protected AbstractRepo(LiteDatabase db)
        {
            Collection = db.GetCollection<T>(CollectionName);
        }

        public void Save(T obj)
        {
            if (obj.Id.Equals(Guid.Empty))
            {
                throw new Exception("Guid.Empty used as identifier");
            }

            Collection.Upsert(obj);
        }

        public T GetOne(Guid id)
        {
            var obj = Collection.FindById(id);
            return obj;
        }

        public IEnumerable<T> GetChildren(Guid parentId)
        {
            return GetMany(Query.EQ("ParentId", parentId));
        }

        public IEnumerable<T> GetMany(BsonExpression q, int skip = 0, int limit = Int32.MaxValue)
        {
            var items = Collection.Find(q, skip, limit);
            return items;
        }

        public T GetOne(BsonExpression q)
        {
            var item = Collection.FindOne(q);
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return Collection
                .FindAll();
        }

        public void Delete(Guid id)
        {
            Collection.Delete(id);
        }
    }
}
