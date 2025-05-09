using LoanTracker.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Infrastructure.DBContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Contact> Contacts => Set<Contact>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Loan>().HasKey(x => x.Id);
            builder.Entity<Contact>().HasKey(x => x.Id);

            builder.Entity<Loan>()
                .HasOne(l => l.Contact)
                .WithMany()
                .HasForeignKey(l => l.ContactId);
        }
    }

}
