using LoanTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Application.Interface
{
    public interface ILoanRepository
    {
        Task<Loan?> GetByIdAsync(Guid id);
        Task<List<Loan>> GetDueLoansAsync(DateTime date);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
    }
}
