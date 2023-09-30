using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechniciansRepo.Models;

namespace TechniciansRepo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TechniciansRepo.Models.KnowledgeBase> KnowledgeBase { get; set; } = default!;
    }
}