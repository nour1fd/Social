namespace Social.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Social.Data;
    using Social.Dtos.Stock;
    using Social.Helpers;
    using Social.Interfaces;
    using Social.Mappers;

    //[Route("api/comment")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IStockRepository stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            this._context = context;
            this.stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stocks = await stockRepository.GetAllAsync(queryObject);
            var stovkDtos = stocks.Select(s => s.ToStockDtos()).ToList();
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stocks = await stockRepository.GetByIdAsync(id);

            return Ok(stocks.ToStockDtos());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockmodel = createStockRequestDto.ToStockFromCreateDtos();

            await stockRepository.CreateAsync(stockmodel);
            return CreatedAtAction(nameof(GetById), new { id = stockmodel.Id }, stockmodel.ToStockDtos());
        }

        [HttpPut("{id:int}")]
        //or 
        //[Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDtos updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel = await stockRepository.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDtos());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel = await stockRepository.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
