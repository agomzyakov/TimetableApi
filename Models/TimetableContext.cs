using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TimetableApi.Models
{
    public class TimetableContext : DbContext
    {
        public TimetableContext(DbContextOptions<TimetableContext> options)
            : base(options)
        {
        }

        public DbSet<TimetableItem> TimetableItems { get; set; } = null!;
    }
}