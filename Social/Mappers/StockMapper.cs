using Social.Dtos.Stock;
using Social.Models;

namespace Social.Mappers
{
    public static class StockMapper
    {
        public static StockDtos ToStockDtos(this Stock stockModel)
        {
            return new StockDtos
            {
                CompanyName = stockModel.CompanyName,
                Id = stockModel.Id,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase,
                Symbol = stockModel.Symbol,
                Comments = stockModel.Comments.Select(c => c.ToCommentDtos()).ToList()
            };

        }
        public static Stock ToStockFromCreateDtos(this CreateStockRequestDto stockModel)
        {
            return new Stock
            {
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase,
                Symbol = stockModel.Symbol,
            };

        }

    }
}
