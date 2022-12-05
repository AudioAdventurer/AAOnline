using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;
using LiteDB;

namespace AudioAdventurer.Library.Data.Interfaces
{
    public interface IRepo<T> where T : IIdentifiableObject
    {
        void Save(T obj);

        T GetOne(Guid id);

        void Delete(Guid id);

        public IEnumerable<T> GetMany(BsonExpression q, int skip = 0, int limit = Int32.MaxValue);

        public T GetOne(BsonExpression q);

        public IEnumerable<T> GetAll();

        public void DeleteMany(BsonExpression q);
    }
}
