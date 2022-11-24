using StartupProject.Core.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace StartupProject.Services.Helpers
{
    /// <summary>
    /// Transaction manager
    /// </summary>
    public class TransactionManager : ITransactionManager
    {
        private IUow _uow;
        private ITransaction _tx;
        private bool isOpen = false;

        private readonly ILogger _log;

        public TransactionManager(IUow uow, ILoggerFactory log)
        {
            _uow = uow;
            _log = log.CreateLogger("ActionTransactionHelper");
        }

        //private bool TransactionHandled { get; set; }
        //private bool SessionClosed { get; set; }

        public void BeginTransaction()
        {
            _tx = _uow.BeginTransaction();
            isOpen = true;
        }

        public void EndTransaction(ActionExecutedContext filterContext)
        {
            if (_tx == null) throw new NotSupportedException();
            if (filterContext.Exception == null)
            {
                if (isOpen)
                {
                    _uow.Commit();
                    _tx.Commit();
                    isOpen = false;
                }

            }
            else
            {
                try
                {
                    _tx.Rollback();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message);
                    throw new AggregateException(filterContext.Exception, ex);
                }

            }

            // TransactionHandled = true;
        }

        public void EndTransaction()
        {
            if (isOpen)
            {
                _uow.Commit();
                _tx.Commit();
                isOpen = false;
            }
            try
            {
                _tx.Rollback();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
            // TransactionHandled = true;
        }

        public void CloseSession()
        {
            if (_tx != null)
            {
                _tx.Dispose();
                _tx = null;
            }

            if (_uow != null)
            {
                _uow.Dispose();
                _uow = null;
            }

            //  SessionClosed = true;
        }
    }
}