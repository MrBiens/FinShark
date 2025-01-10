using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository 
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(CreateStockRequest stockRequest);

        Task<Stock?> UpdateAsync(int id,UpdateStockRequest updateStock);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExistById(int id);
    }
}