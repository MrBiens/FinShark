using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository :ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public CommentRepository(ApplicationDBContext context,IStockRepository stockRepo)
        {
            _context=context;
            _stockRepo=stockRepo;
        }

 

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync() ;
        }

        public async Task<Comment?> GetByIdAsync(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<Comment> CreateAsync(CreateCommentRequest createRequest,int stockId)
        {
            if( !await _stockRepo.StockExistById(stockId)){
                 throw new ArgumentException("Stock does not exist.");
            }

            Comment commentModel = createRequest.ToComment(stockId);
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
            
        }

        public async Task<Comment?> UpdateAsync(int commentId, UpdateCommentRequest updateCommentRequest)
        {
            var commentModel = await GetByIdAsync(commentId);
            if(commentModel == null){
                throw new ArgumentException("Comment does not exist");
            }

            commentModel.UpdateComment(updateCommentRequest);

            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int commentId)
        {
            var commentModel = await GetByIdAsync(commentId);
            if(commentModel == null){
                throw new ArgumentException("Comment does not exist");
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
    

        }
    }
}