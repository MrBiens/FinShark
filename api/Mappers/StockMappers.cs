using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel){
            return new StockDto{
                Id = stockModel.Id,
                Symbol=stockModel.Symbol,
                CompanyName=stockModel.CompanyName,
                Purchase=stockModel.Purchase,
                LastDiv=stockModel.LastDiv,
                Industry=stockModel.Industry,
                MarketCap=stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStock(this CreateStockRequest stockRequest){
            return new Stock{
                Symbol=stockRequest.Symbol,
                CompanyName=stockRequest.CompanyName,
                Purchase=stockRequest.Purchase,
                LastDiv=stockRequest.LastDiv,
                Industry=stockRequest.Industry,
                MarketCap=stockRequest.MarketCap
            };
        }

        /*
         public static void UpdateStock( UpdateStockRequest stockRequest,Stock stockModel){
                stockModel.Symbol=stockRequest.Symbol;
                stockModel.CompanyName=stockRequest.CompanyName;
                stockModel.Purchase=stockRequest.Purchase;
                stockModel.LastDiv=stockRequest.LastDiv;
                stockModel.Industry=stockRequest.Industry;
                stockModel.MarketCap=stockRequest.MarketCap;
        }
        */






        
    }
}