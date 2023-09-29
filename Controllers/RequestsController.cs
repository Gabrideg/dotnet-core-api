using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RequestsApi.Models;

namespace RequestsApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class requestsController : Controller
    {
        private readonly RequestsContext _context;

        public requestsController(RequestsContext context)
        {
            _context = context;

            if (_context.RequestsItems.Count() == 0)
            {
                _context.RequestsItems.Add(new RequestsItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestsItem>>> GetRequestsItem()
        {
            return await _context.RequestsItems.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestsItem>> GetRequestsItem(long id)
        {
            var RequestsItem = await _context.RequestsItems.FindAsync(id);

            if (RequestsItem == null)
            {
                return NotFound();
            }

            return RequestsItem;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestsItem(long id, RequestsItem RequestsItem)
        {
            if (id != RequestsItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(RequestsItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RequestsItem>> PostRequestsItem(RequestsItem RequestsItem)
        {
            _context.RequestsItems.Add(RequestsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestsItem", new { id = RequestsItem.Id }, RequestsItem);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestsItem>> DeleteRequestsItem(long id)
        {
            var RequestsItem = await _context.RequestsItems.FindAsync(id);
            if (RequestsItem == null)
            {
                return NotFound();
            }

            _context.RequestsItems.Remove(RequestsItem);
            await _context.SaveChangesAsync();

            return RequestsItem;
        }

        private bool RequestsItemExists(long id)
        {
            return _context.RequestsItems.Any(e => e.Id == id);
        }
    }
}
