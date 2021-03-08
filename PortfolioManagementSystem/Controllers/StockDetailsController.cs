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
        public async Task<ActionResult<IEnumerable<StockDetailViewModel>>> GetStockDetails()
        {
            var dbStockDetails= await _context.StockDetails.ToListAsync();

            var stockDetailsView = new List<StockDetailViewModel>();

            foreach(var stockDetails in dbStockDetails)
            {
                var stocks = await _context.Stocks.FindAsync(stockDetails.StockId);
                stockDetailsView.Add(new StockDetailViewModel
                {
                    TransactionId = stockDetails.TransactionId,
                    TransactionType = stockDetails.TransactionType,
                    Quantity = stockDetails.Quantity,
                    Amount = stockDetails.Amount,
                    TransactionDate = stockDetails.TransactionDate.ToString("dd-MM-yyyy"),
                    StockId = stockDetails.StockId,
                    StockName = stocks.StockName
                });
            }

            return stockDetailsView;
        }

        // GET: api/StockDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockDetailViewModel>> GetStockDetails(int id)
        {
            var stockDetails = await _context.StockDetails.FindAsync(id);

            if (stockDetails == null)
            {
                return NotFound();
            }

            var stocks= await _context.Stocks.FindAsync(stockDetails.StockId);

            var stockDetailViewModel = new StockDetailViewModel
            {
                TransactionId = stockDetails.TransactionId,
                TransactionType = stockDetails.TransactionType,
                Quantity = stockDetails.Quantity,
                Amount=stockDetails.Amount,
                TransactionDate=stockDetails.TransactionDate.ToString("dd-MM-yyyy"),
                StockId=stockDetails.StockId,
                StockName = stocks.StockName
            };

            return stockDetailViewModel;
        }

        // PUT: api/StockDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockDetails(int id, StockDetails stockDetails)
        {
            if (id != stockDetails.TransactionId)
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
            if (stockDetails.TransactionType == "sale")
            {
                var listDbStockDetails = await _context.StockDetails.Where(x => x.StockId == stockDetails.StockId).ToListAsync();
                if (listDbStockDetails==null)
                {
                    return NotFound();
                }
                else
                {
                    int quantity=0;
                    foreach(var listDbStockDetail in listDbStockDetails)
                    {
                        if (listDbStockDetail.TransactionType == "buy")
                            quantity += listDbStockDetail.Quantity;
                        else
                            quantity -= listDbStockDetail.Quantity;
                    }

                    if(quantity<stockDetails.Quantity)
                        return BadRequest();
                    else
                    {
                        _context.StockDetails.Add(stockDetails);
                        await _context.SaveChangesAsync();
                        return CreatedAtAction("GetStockDetails", new { id = stockDetails.TransactionId }, stockDetails);
                    }

                }
            }
            _context.StockDetails.Add(stockDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockDetails", new { id = stockDetails.TransactionId }, stockDetails);
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
            return _context.StockDetails.Any(e => e.TransactionId == id);
        }

        [HttpGet]
        [Route("getprofit")]
        public async Task<ActionResult<StockProfitViewModel>> GetProfit()
        {
            var dbStockDetails = await _context.StockDetails.ToListAsync();
            var stockProfit = new StockProfitViewModel();
            var boughtUnits = 0;
            var soldUnits =0;

            foreach(var stockDetails in dbStockDetails)
            {
                if (stockDetails.TransactionType == "buy")
                {
                    stockProfit.TotalInvestment += (stockDetails.Amount * stockDetails.Quantity);
                    boughtUnits += stockDetails.Quantity;
                }
                else
                {
                    stockProfit.SoldAmount += (stockDetails.Amount * stockDetails.Quantity);
                    soldUnits += stockDetails.Quantity;
                }
                stockProfit.TotalUnits += stockDetails.Quantity;
            }

            stockProfit.CurrentAmount = (boughtUnits - soldUnits) * dbStockDetails[dbStockDetails.Count-1].Amount;
            stockProfit.Profit = stockProfit.SoldAmount - stockProfit.TotalInvestment;

            return stockProfit;
        }

        [HttpGet]
        [Route("getprofitsingle")]
        public async Task<ActionResult<IEnumerable<StockProfitSingleViewModel>>> GetProfitSingle()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var singleStockProfit = new List<StockProfitSingleViewModel>();


            foreach (var stock in stocks)
            {
                double investment = 0;
                double sales = 0;
                double currentAmount = 0;
                var totalUnits = 0;
                var boughtUnits = 0;
                var soldUnits = 0;
                double lastAmount = 0;
                var listDbStockDetails = await _context.StockDetails.Where(x => x.StockId == stock.StockId).ToListAsync();
                if (listDbStockDetails != null)
                {
                    foreach (var stockDetails in listDbStockDetails)
                    {
                        if (stockDetails.TransactionType == "buy")
                        {
                            investment += (stockDetails.Amount * stockDetails.Quantity);
                            boughtUnits += stockDetails.Quantity;
                        }
                        else
                        {
                            sales += (stockDetails.Amount * stockDetails.Quantity);
                            soldUnits += stockDetails.Quantity;
                        }
                        totalUnits += stockDetails.Quantity;
                        lastAmount = stockDetails.Amount;
                    }

                    currentAmount = (boughtUnits - soldUnits) * lastAmount;
                    singleStockProfit.Add(new StockProfitSingleViewModel
                    {
                        StockName = stock.StockName,
                        TotalUnits = totalUnits,
                        TotalInvestment = investment,
                        SoldAmount = sales,
                        CurrentAmount = currentAmount,
                        Profit = sales - investment
                    });
                }
                
            }
            return singleStockProfit;
        }
    }
}
