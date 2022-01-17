using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;

namespace SuperTraders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly IShareRepository _shareRepository;

        public SharesController(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }

        // GET: api/Shares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Share>>> GetShares()
        {
            return await _shareRepository.GetShareList();
        }

 

        // GET: api/Shares/5
        [HttpGet("{symbol}")]
        public async Task<ActionResult<Share>> GetShare(string symbol)
        {
            var share = await _shareRepository.GetShareBySymbol(symbol);

            if (share == null)
            {
                return NotFound();
            }

            return share;
        }

   
        [HttpPut("{symbol}")]
        public async Task<IActionResult> PutShare(string symbol, Share share)
        {
            if (symbol != share.Symbol)
            {
                return BadRequest();
            }

            if (!ShareExists(symbol))
                return BadRequest($"Requested Symbol : {symbol} Not Exist!");

            if(!await _shareRepository.IsRateUpdateAvailable(symbol))
                return BadRequest($"Share Update only hourly basis available!");


            await _shareRepository.UpdateShareBySymbol(symbol, share);

            return CreatedAtAction("GetShare", new { symbol = share.Symbol }, share);
        }

        // POST: api/Shares
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Share>> PostShare(Share share)
        {

            if (_shareRepository.IsShareExist(share.Symbol))
                return BadRequest($"Requested Symbol:{share.Symbol} Already Exist!");

            await _shareRepository.AddShare(share);

            return CreatedAtAction("GetShare", new {symbol = share.Symbol}, share);
        }

        // DELETE: api/Shares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShare(int id)
        {
            await _shareRepository.RemoveShareById(id);

            return NoContent();
        }

        private bool ShareExists(string symbol)
        {
            return _shareRepository.IsShareExist(symbol);
        }
    }
}