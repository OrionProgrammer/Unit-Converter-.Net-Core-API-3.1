using System;
using System.Threading.Tasks;
using App.Repository.Interfaces;

namespace App.Repository.Helpers
{
    public interface IUnitOfWork 
    {
        IAuditLogRepository AuditLog { get; }

        IUserRepository User { get; }

        Task<int> Complete();
    }
}
