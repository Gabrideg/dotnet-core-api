using Microsoft.EntityFrameworkCore;

namespace RequestsApi.Models
{
    public class RequestsContext : DbContext
    {
        public RequestsContext (DbContextOptions<RequestsContext> options)
            : base(options)
        {
        }

        public DbSet<RequestsItem> RequestsItems { get; set; }
    }
}
