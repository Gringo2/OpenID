using Microsoft.EntityFrameworkCore;
using OpenID.Entities;

namespace OpenID.DbContexts.Interfaces
{
    public interface IAdminLogDbContext
    {
        DbSet<Log> Logs { get; set; }
    }
}
