
using LoanTracker.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoanTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var contact = await _contactService.GetContactAsync(id);
            return contact != null ? Ok(contact) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _contactService.GetAllContactsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDto dto)
        {
            var contact = await _contactService.CreateContactAsync(dto.Name, dto.Email);
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }
        
        public record ContactDto(string Name, string Email);
    }
}