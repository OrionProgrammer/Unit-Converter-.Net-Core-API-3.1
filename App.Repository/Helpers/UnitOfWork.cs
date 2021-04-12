using System;
using App.Repository.Interfaces;
using App.Domain.Helpers;
using System.Threading.Tasks;

namespace App.Repository.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IAuditLogRepository AuditLog { get; }

        public IUserRepository User { get; }

        public UnitOfWork(DataContext dataContext, 
            IAuditLogRepository auditLogRepository,
            IUserRepository userRepository)
        {
            this._context = dataContext;
            this.AuditLog = auditLogRepository;
            this.User = userRepository;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
