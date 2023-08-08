using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITTechniciansRepo.Models;

namespace ITTechniciansRepo.Data
{
    public class ITTechniciansRepoContext : DbContext
    {
        public ITTechniciansRepoContext (DbContextOptions<ITTechniciansRepoContext> options)
            : base(options)
        {
        }

        public DbSet<ITTechniciansRepo.Models.KnowledgeBase> KnowledgeBase { get; set; } = default!;
    }
}
