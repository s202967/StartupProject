using StartupProject.Core.Infrastructure.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.EntityFramework
{
    public class UnitOfWork : IUow
    {
        protected ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new DbTransaction(_dbContext.Database.CurrentTransaction ?? _dbContext.Database.BeginTransaction());
        }

        public void Commit()
        {
            _dbContext.Commit();
        }

        public async Task CommitAsync(bool isSoftDelete = true)
        {
            try
            {
                await _dbContext.CommitAsync(isSoftDelete).ConfigureAwait(false);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }

    public class Repository<T> : UnitOfWork, IRepository<T> where T : class
    {

        #region Properties

        private readonly DbSet<T> _dbSet;

        #endregion

        protected Repository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        #region Implementation

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(List<T> itemList)
        {
            foreach (T obj in itemList)
            {
                _dbSet.Attach(obj);
                _dbContext.Entry(obj).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual void DeleteList(IQueryable<T> itemList)
        {
            foreach (T obj in itemList)
                _dbSet.Remove(obj);
        }

        public virtual void DeleteRange(List<T> itemList)
        {
            _dbSet.RemoveRange(itemList);
        }

        public virtual void AddRange(List<T> itemList)
        {
            _dbSet.AddRange(itemList);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsNoTrackingAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync().ConfigureAwait(false);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public int CountAll(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        }

        public bool IsExist(Expression<Func<T, bool>> where)
        {
            return _dbSet.Any(where);
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return _dbSet;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return _dbSet.AsNoTracking();
            }
        }

        public async Task ExecuteNonQueryAsync(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                await _dbContext.Database.OpenConnectionAsync();
            }
            var command = _dbContext.Database.GetDbConnection().CreateCommand();

            if (_dbContext.Database.CurrentTransaction != null)
                command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();

            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public ICollection<T> ExecuteReader(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                _dbContext.Database.OpenConnection();
            }

            var command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (_dbContext.Database.CurrentTransaction != null)
                command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
                while (reader.NextResult()) { }

            var mapper = new DataReaderMapper<T>();
            return mapper.MapToList(reader);
        }

        public async Task<ICollection<TOutput>> ExecuteReaderAsync<TOutput>(string commandText, CommandType commandType, SqlParameter[] parameters = null) where TOutput : class
        {
            if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                _dbContext.Database.OpenConnection();
            }

            var command = _dbContext.Database.GetDbConnection().CreateCommand();

            if (_dbContext.Database.CurrentTransaction != null)
                command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();

            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            using DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            if (!reader.HasRows)
                while (await reader.NextResultAsync()) { }

            var mapper = new DataReaderMapper<TOutput>();
            return mapper.MapToList(reader);
        }

        public ICollection<TOutput> ExecuteReader<TOutput>(string commandText, CommandType commandType, SqlParameter[] parameters = null) where TOutput : class
        {
            if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                _dbContext.Database.OpenConnection();
            }

            var command = _dbContext.Database.GetDbConnection().CreateCommand();

            if (_dbContext.Database.CurrentTransaction != null)
                command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();

            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            using DbDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
                while (reader.NextResult()) { }

            var mapper = new DataReaderMapper<TOutput>();
            return mapper.MapToList(reader);
        }

        protected virtual async Task<T> ExecuteReaderAsync<T>(Func<DbDataReader, T> mapEntities, string exec, SqlParameter[] parameters = null)
        {

            if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                _dbContext.Database.OpenConnection();
            }

            var command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = exec;

            if (_dbContext.Database.CurrentTransaction != null)
                command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            if (!reader.HasRows)
                while (await reader.NextResultAsync()) { }

            T data = mapEntities(reader);
            return data;
        }

        #endregion

    }
}
