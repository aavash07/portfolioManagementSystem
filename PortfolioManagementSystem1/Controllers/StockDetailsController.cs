using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioManagementSystem.Models;

namespace PortfolioManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDetailsController : ControllerBase
    {
        private readonly StockDbContext _context;

        public StockDetailsController(StockDbContext context)
        {
            _context = context;
        }

        // GET: api/StockDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDetails>>> GetStockDetails()
        {
            return await _context.StockDetails.ToListAsync();
        }

        // GET: api/StockDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockDetails>> GetStockDetails(int id)
        {
            var stockDetails = await _context.StockDetails.FindAsync(id);

            if (stockDetails == null)
            {
                return NotFound();
            }

            return stockDetails;
        }

        // PUT: api/StockDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockDetails(int id, StockDetails stockDetails)
        {
            if (id != stockDetails.StockId)
            {
                return BadRequest();
            }

            _context.Entry(stockDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockDetailsExists(id))
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

        // POST: api/StockDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockDetails>> PostStockDetails(StockDetails stockDetails)
        {
            _context.StockDetails.Add(stockDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockDetails", new { id = stockDetails.StockId }, stockDetails);
        }

        // DELETE: api/StockDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockDetails(int id)
        {
            var stockDetails = await _context.StockDetails.FindAsync(id);
            if (stockDetails == null)
            {
                return NotFound();
            }

            _context.StockDetails.Remove(stockDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockDetailsExists(int id)
        {
            return _context.StockDetails.Any(e => e.StockId == id);
        }
    }
}
