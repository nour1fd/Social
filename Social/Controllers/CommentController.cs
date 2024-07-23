using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social.Data;
using Social.Dtos.Comment;
using Social.Extensions;
using Social.Interfaces;
using Social.Mappers;
using Social.Models;

namespace Social.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository commentRepository;
        private readonly IStockRepository stockRepository;
        private readonly UserManager<AppUser> userManager;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, 
            IStockRepository stockRepository,UserManager<AppUser> userManager)
        {
            this._context = context;
            this.commentRepository = commentRepository;
            this.stockRepository = stockRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var commentModel = await commentRepository.GetAllAsync();
            var CommentModelDtos = commentModel.Select(s => s.ToCommentDtos());
            return Ok(CommentModelDtos);
        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var CommentModel = await commentRepository.GetByIdAsync(id);
            if (CommentModel == null)
            {
                return NotFound();
            }
            return Ok(CommentModel.ToCommentDtos());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!await stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock Dosnot exist");
            }
            var userName =  User.GetUsername();
            var appUser = await userManager.FindByNameAsync(userName);


            var commentModel = createCommentDto.ToCommentfromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            await commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetByID), new { id = commentModel.Id }, commentModel.ToCommentDtos());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var CommentModel = await commentRepository.UpdateAsync(id, updateCommentRequestDto.ToCommentFromUpdate());
            if (CommentModel == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(CommentModel.ToCommentDtos());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var commentModel = commentRepository.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("No Commnet");
            }
            return Ok();
        }

    }
}
