using Microsoft.EntityFrameworkCore;
using SportCoachingApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SportCoachingApi.Context
{
    public class SportsCoatchingContext : DbContext
    {
        public SportsCoatchingContext(DbContextOptions<SportsCoatchingContext> options) : base(options)
        {
        }

        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
