using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StartupProject.Core.Infrastructure.DataAccess
{
    public interface IUow : IDisposable
    {
        ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot);

        void Commit();

        Task CommitAsync(bool isSoftDelete = true);
    }

    public interface IRepository<T> : IUow where T : class
    {
        // Marks an entity as new
        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(List<T> itemList);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void UpdateRange(List<T> entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetById(int id);

        T GetById(long id);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(long id);

        // Get an entity by int id as no tracking
        Task<T> GetByIdAsNoTrackingAsync(int id);

        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);

        Task<T> GetAsync(Expression<Func<T, bool>> where);

        void DeleteList(IQueryable<T> itemList);

        void DeleteRange(List<T> itemList);

        // Gets all entities of type T
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> GetAll(Expression<Func<T, bool>> where);

        // Gets entities using delegate
        int CountAll(Expression<Func<T, bool>> where);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);

        bool IsExist(Expression<Func<T, bool>> where);

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }

        // 1. SqlQuery approach
        //ICollection<T> ExcuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null);

        //ICollection<TOutput> ExcuteSqlQuery<TOutput>(string sqlQuery, CommandType commandType,
        //    SqlParameter[] parameters = null);

        // 2. SqlCommand approach
        // void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null);

        //  ICollection<T> ExecuteReader(string commandText, CommandType commandType, SqlParameter[] parameters = null);

        // ICollection<TOutput> ExecuteReader<TOutput>(string commandText, CommandType commandType, SqlParameter[] parameters = null) where TOutput : class;

        Task<ICollection<TOutput>> ExecuteReaderAsync<TOutput>(string commandText, CommandType commandType, SqlParameter[] parameters = null) where TOutput : class;
    }
}
