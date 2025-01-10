using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel){
            return new CommentDto{
                Id=commentModel.Id,
                Title=commentModel.Title,
                Content=commentModel.Content,
                CreateOn=commentModel.CreateOn,
                StockId=commentModel.StockId
            };
        }

        

        public static Comment ToComment(this CreateCommentRequest createComment,int stockId){
            return new Comment{
                Title = createComment.Title,
                Content=createComment.Content,
                StockId = stockId     
            };
        }

        public static void UpdateComment(this Comment commentModel,UpdateCommentRequest updateCommentRequest){
            if (!string.IsNullOrEmpty(updateCommentRequest.Title))
            {
                commentModel.Title = updateCommentRequest.Title;
            }

            if (!string.IsNullOrEmpty(updateCommentRequest.Content))
            {
                commentModel.Content = updateCommentRequest.Content;
            }
        }



    }
}