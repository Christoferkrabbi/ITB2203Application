using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Controllers
{
    public class SessionController : Controller
    {
        private readonly DataContext context;
        public SessionController(DataContext c)
        {
            context = c;
        }
        [HttpGet]
        public IActionResult GetSessions()
        {
            var sessions = context.SessionList!.AsQueryable();
            return Ok(sessions);

        }
        [HttpPost]
        public IActionResult CreateSession([FromBody] Session e)
        {
            var dbSession = context.SessionList?.Find(e.Id);
            if (dbSession == null)
            {
                context.SessionList?.Add(e);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetSessions), new { e.Id }, e);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Session e)
        {
            var dbSession = context.SessionList!.AsNoTracking().FirstOrDefault(sessionInDB => sessionInDB.Id == e.Id);
            if (id != e.Id || dbSession == null) return NotFound();
            context.Update(e);
            context.SaveChanges();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sessionToDelete = context.SessionList?.Find(id);
            if (sessionToDelete == null) return NotFound();
            context.SessionList?.Remove(sessionToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}
