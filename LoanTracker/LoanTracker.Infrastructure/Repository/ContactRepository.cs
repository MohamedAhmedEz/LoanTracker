using LoanTracker.Application.Interface;
using LoanTracker.Domain.Entity;
using LoanTracker.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LoanTracker.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _ctx;

        public ContactRepository(AppDbContext context)
        {
            _ctx = context;
        }

        public async Task<Contact> GetByIdAsync(Guid id)
            => await _ctx.Contacts.FindAsync(id);

        public async Task<IEnumerable<Contact>> GetAllAsync()
            => await _ctx.Contacts.ToListAsync();

        public async Task AddAsync(Contact contact)
        {
            await _ctx.Contacts.AddAsync(contact);
            await _ctx.SaveChangesAsync();
        }

        
    }
}