using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Controllers
{
    public class TicketController : Controller
    {
        private readonly DataContext context;
        public TicketController(DataContext c)
        {
            context = c;
        }
        [HttpGet]
        public IActionResult GetTickets()
        {
            var tickets = context.TicketList!.AsQueryable();
            return Ok(tickets);

        }
        [HttpPost]
        public IActionResult CreateTicket([FromBody] Ticket e)
        {
            var dbTicket = context.TicketList?.Find(e.Id);
            if (dbTicket == null)
            {
                context.TicketList?.Add(e);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetTickets), new { e.Id }, e);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Ticket e)
        {
            var dbTicket = context.TicketList!.AsNoTracking().FirstOrDefault(ticketInDB => ticketInDB.Id == e.Id);
            if (id != e.Id || dbTicket == null) return NotFound();
            context.Update(e);
            context.SaveChanges();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticketToDelete = context.TicketList?.Find(id);
            if (ticketToDelete == null) return NotFound();
            context.TicketList?.Remove(ticketToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}
