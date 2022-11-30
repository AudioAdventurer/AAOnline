using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Data.Interfaces
{
    internal interface IRepo<T> where T : IIdentifiableObject
    {
        void Save(T obj);

        T GetOne(Guid id);

        void Delete(Guid id);
    }
}
