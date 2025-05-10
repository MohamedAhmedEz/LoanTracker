using LoanTracker.Application.Interface;
using LoanTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Infrastructure.Sender
{
    public class ConsoleNotificationService : INotificationService
    {
        public Task SendReminderAsync(Loan loan)
        {
            Console.WriteLine($"Reminder: Loan {loan.Id} is due on {loan.DueDate:d}.");
            return Task.CompletedTask;
        }
    }
}
