
using api.Dtos.Comment;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
        {
            _commentRepo=commentRepo;
            _stockRepo=stockRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(){

            var comments=await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "id")] int commentId){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(commentId);
            if(comment == null){
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
        
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute(Name =("stockId"))] int stockId,[FromBody] CreateCommentRequest createRequest){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var commentModel =  await _commentRepo.CreateAsync(createRequest,stockId);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},commentModel.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute(Name = "id")] int commentId,[FromBody] UpdateCommentRequest updateCommentRequest){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.UpdateAsync(commentId,updateCommentRequest);
            if(commentModel == null){
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int commentId){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.DeleteAsync(commentId);
            if(commentModel == null){
                return NotFound();
            }
            return NoContent();
        }


    }
}