using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Domain.Entity
{
    public class Loan
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public decimal AmountPaid { get; private set; }
        public DateTime DueDate { get; private set; }
        public Guid ContactId { get; private set; }

        public Contact Contact { get; private set; }

        public bool IsPaid => AmountPaid >= Amount;

        private Loan() { } // EF Core

        public Loan(string description, decimal amount, DateTime dueDate, Guid contactId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            DueDate = dueDate;
            ContactId = contactId;
            AmountPaid = 0;
        }

        public void MarkAsPaid() => AmountPaid = Amount;

        public void AddPartialPayment(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Invalid amount");
            AmountPaid = Math.Min(AmountPaid + amount, Amount);
            if (AmountPaid >= Amount)MarkAsPaid();
        }
    }

}
