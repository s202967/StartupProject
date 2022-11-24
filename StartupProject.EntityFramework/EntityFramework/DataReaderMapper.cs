using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace StartupProject.EntityFramework.EntityFramework
{
    public class DataReaderMapper<T> where T : class
    {
        public List<T> MapToList(DbDataReader dr)
        {
            var entities = new List<T>();
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);

                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(CultureInfo.CurrentCulture), p => p);

                T newObject = default;
                while (dr.Read())
                {
                    newObject = (T)Activator.CreateInstance(typeof(T));

                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper(CultureInfo.CurrentCulture)))
                        {
                            var info = propDict[dr.GetName(index).ToUpper(CultureInfo.CurrentCulture)];
                            if (info != null && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, val == DBNull.Value ? null : val, null);
                            }
                        }
                    }

                    entities.Add(newObject);
                }

                return entities;
            }

            return entities;
        }
    }
}
