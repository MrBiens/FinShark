using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks= _context.Stocks.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Symbol)){
                stocks=stocks.Where(s=> s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(query.CompanyName)){
                stocks=stocks.Where(s=> s.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                    stocks = query.IsDecsending?stocks.OrderByDescending(s => s.Symbol):stocks.OrderBy(s => s.Symbol);
                }
            }
            var pageNumber = query.PageNumber >= 0 ? query.PageNumber : 0;
            var skipNumber = pageNumber > 0 ? (pageNumber - 1) * query.PageSize : 0 * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int stockId)
        {
           return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == stockId);
        }

            public async Task<bool> StockExistById(int stockId)
        {
            return await _context.Stocks.AnyAsync(s => s.Id==stockId);
        }


        public async Task<Stock> CreateAsync(CreateStockRequest stockRequest)
        {
            Stock stockModel = stockRequest.ToStock();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        
        public async Task<Stock?> UpdateAsync(int stockId, UpdateStockRequest updateStock)
        {
            var exitsingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);
            if(exitsingStock == null){
                return null;
            }
                exitsingStock.Symbol=updateStock.Symbol;
                exitsingStock.CompanyName=updateStock.CompanyName;
                exitsingStock.Purchase=updateStock.Purchase;
                exitsingStock.LastDiv=updateStock.LastDiv;
                exitsingStock.Industry=updateStock.Industry;
                exitsingStock.MarketCap=updateStock.MarketCap;

                await _context.SaveChangesAsync();
                return exitsingStock;
        }

        public async Task<Stock?> DeleteAsync(int stockId)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

    
    }
}