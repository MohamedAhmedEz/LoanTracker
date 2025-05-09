using LoanTracker.Application.Interface;
using LoanTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Application.Service
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepo;

        public LoanService(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }

        public async Task<Guid> AddLoanAsync(string desc, decimal amount, DateTime dueDate, Guid contactId)
        {
            var loan = new Loan(desc, amount, dueDate, contactId);
            await _loanRepo.AddAsync(loan);
            return loan.Id;
        }

        public async Task MarkAsPaidAsync(Guid loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId)
                       ?? throw new Exception("Loan not found");

            loan.MarkAsPaid();
            await _loanRepo.UpdateAsync(loan);
        }

        public async Task AddPartialPaymentAsync(Guid loanId, decimal amount)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId)
                       ?? throw new Exception("Loan not found");

            loan.AddPartialPayment(amount);
            await _loanRepo.UpdateAsync(loan);
        }

        public Task<List<Loan>> GetUpcomingDueLoansAsync(long days) =>
            _loanRepo.GetDueLoansAsync(DateTime.UtcNow.AddDays(days));
    }

}
