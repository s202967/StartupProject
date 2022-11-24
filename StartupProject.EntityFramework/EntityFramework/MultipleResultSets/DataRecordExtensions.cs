using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace StartupProject.EntityFramework.EntityFramework.MultipleResultSets
{
    public static class DataRecordExtensions
    {
        private static readonly ConcurrentDictionary<Type, object> _materializers = new ConcurrentDictionary<Type, object>();

        public static IList<T> Translate<T>(this DbDataReader reader) where T : new()
        {
            var materializer = (Func<IDataRecord, T>)_materializers.GetOrAdd(typeof(T), (Func<IDataRecord, T>)Materializer.Materialize<T>);
            return reader.Translate(materializer, out var hasNextResults);
        }

        public static IList<T> Translate<T>(this DbDataReader reader, Func<IDataRecord, T> objectMaterializer)
        {
            return reader.Translate(objectMaterializer, out _);
        }

        public static IList<T> Translate<T>(this DbDataReader reader, Func<IDataRecord, T> objectMaterializer, out bool hasNextResult)
        {
            var results = new List<T>();
            while (reader.Read())
            {
                var record = (IDataRecord)reader;
                var obj = objectMaterializer(record);
                results.Add(obj);
            }
            hasNextResult = reader.NextResult();
            return results;
        }
    }
}
