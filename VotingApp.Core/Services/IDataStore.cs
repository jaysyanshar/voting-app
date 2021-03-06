using System;
using System.Collections.Generic;

namespace VotingApp.Core.Services
{
    public interface IDataStore<in TKey, TValue> where TValue : class
    {
        bool Add( TValue data );

        TValue Get( TKey id );

        IEnumerable<TValue> GetMany( Predicate<TValue> predicate );

        bool Update( TKey id, TValue data );

        bool Remove( TKey id );
    }
}