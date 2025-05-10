using LoanTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanTracker.Application.Interface
{
    public interface INotificationService
    {
        Task SendReminderAsync(Loan loan);
    }
}
