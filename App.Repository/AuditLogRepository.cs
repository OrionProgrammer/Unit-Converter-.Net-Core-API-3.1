using App.Domain;
using App.Repository.Helpers;
using App.Domain.Helpers;
using App.Repository.Interfaces;

namespace App.Repository
{
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(DataContext context) : base(context)
        {
            
        }

    }
}
