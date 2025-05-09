using LoanTracker.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoanTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _service;

        public LoansController(LoanService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] AddLoanDto dto)
        {
            var id = await _service.AddLoanAsync(dto.Description, dto.Amount, dto.DueDate, dto.ContactId);
            return Ok(new { id });
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(Guid id)
        {
            await _service.MarkAsPaidAsync(id);
            return Ok();
        }

        [HttpPost("{id}/partial")]
        public async Task<IActionResult> Partial(Guid id, [FromBody] decimal amount)
        {
            await _service.AddPartialPaymentAsync(id, amount);
            return Ok();
        }

        [HttpGet("due/{days}")]
        public async Task<IActionResult> DueLoans(long days)
        {
            var loans = await _service.GetUpcomingDueLoansAsync(days);
            return Ok(loans);
        }
    }

    public class AddLoanDto
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ContactId { get; set; }
    }

}
