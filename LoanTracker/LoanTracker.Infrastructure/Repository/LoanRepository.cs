using LoanTracker.Application.Interface;
using LoanTracker.Domain.Entity;
using LoanTracker.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Infrastructure.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _ctx;

        public LoanRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<Loan?> GetByIdAsync(Guid id) =>
            _ctx.Loans.Include(l => l.Contact).FirstOrDefaultAsync(l => l.Id == id);

        public Task<List<Loan>> GetDueLoansAsync(DateTime date) =>
            _ctx.Loans.Include(l => l.Contact)
                .Where(l => l.DueDate <= date && (l.Amount > l.AmountPaid))
                .ToListAsync();

        public async Task AddAsync(Loan loan)
        {
            _ctx.Loans.Add(loan);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loan loan)
        {
            _ctx.Loans.Update(loan);
            await _ctx.SaveChangesAsync();
        }
    }

}
