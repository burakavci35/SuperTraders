using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Repository
{
    public class ShareRepository : IShareRepository
    {
        private readonly SuperTradersContext _context;
        private readonly int _rateUpdateAvailableHours = 1;

        public ShareRepository(SuperTradersContext context)
        {
            _context = context;
        }

        public Task<List<Share>> GetShareList()
        {
            return _context.Shares.ToListAsync();
        }

        public async Task<decimal> CalculateTotalOfShares(string symbol, decimal amount)
        {
            var foundShare = await GetShareBySymbol(symbol);

            return foundShare.Rate * amount;
        }

        public async Task<Share> GetShareById(int id)
        {
            var foundShare =  await _context.Shares.FindAsync(id);

            return foundShare;
        }

        public async Task<Share> GetShareBySymbol(string symbol)
        {
            var foundShare = await _context.Shares.FirstOrDefaultAsync(x=>x.Symbol == symbol);
                
            return foundShare;
        }


        public async Task AddShare(Share share)
        {
            _context.Shares.Add(share);

             await _context.SaveChangesAsync();
        }

        public async Task UpdateShareBySymbol(string symbol, Share share)
        {
            var foundShare = await _context.Shares.FirstOrDefaultAsync(x=>x.Symbol == symbol);

            foundShare.Symbol = share.Symbol;
            foundShare.Rate = share.Rate;
            foundShare.LastUpDateTime = DateTime.Now;

            await _context.SaveChangesAsync();
        }


        public async Task UpdateShareById(int id, Share share)
        {
            var foundShare = await _context.Shares.FindAsync(id);

            foundShare.Symbol = share.Symbol;
            foundShare.Rate = share.Rate;
            foundShare.LastUpDateTime = foundShare.LastUpDateTime;

            await _context.SaveChangesAsync();
        }



        public async Task RemoveShareById(int id)
        {
            var foundShare = await _context.Shares.FindAsync(id);

            _context.Remove(foundShare);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsRateUpdateAvailable(string symbol)
        {
            var foundShare = await _context.Shares.FirstOrDefaultAsync(x => x.Symbol == symbol);

            return foundShare.LastUpDateTime.AddHours(_rateUpdateAvailableHours) < DateTime.Now;
        }

     

        public bool IsShareExist(string symbol)
        {
            return _context.Shares.Any(x => x.Symbol == symbol);
        }

        public bool IsShareExist(int id)
        {
            return _context.Shares.Any(x => x.Id == id);
        }
    }
}
