using App.Domain;
using App.Repository.Helpers;

namespace App.Repository.Interfaces
{

    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
    }
}
