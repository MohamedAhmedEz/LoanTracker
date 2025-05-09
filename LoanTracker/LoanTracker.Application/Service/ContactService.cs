using LoanTracker.Application.Interface;
using LoanTracker.Domain.Entity;

namespace LoanTracker.Application.Service
{
    public class ContactService 
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateContactAsync(string name, string email)
        {
            var contact = new Contact(name, email);
            await _contactRepository.AddAsync(contact);
            return contact;
        }

        public async Task<Contact> GetContactAsync(Guid id)
            => await _contactRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
            => await _contactRepository.GetAllAsync();

        
    }
}