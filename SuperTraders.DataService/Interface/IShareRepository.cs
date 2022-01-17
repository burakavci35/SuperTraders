using System.Collections.Generic;
using System.Threading.Tasks;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Interface
{
    public interface IShareRepository
    {
        Task<List<Share>> GetShareList();
        Task<Share> GetShareBySymbol(string symbol);
        Task<decimal> CalculateTotalOfShares(string symbol,decimal amount);
        Task AddShare(Share share);
        Task UpdateShareBySymbol(string symbol,Share share);   
        Task RemoveShareById(int id);
        Task<bool> IsRateUpdateAvailable(string symbol);
        bool IsShareExist(string symbol);
    }
}