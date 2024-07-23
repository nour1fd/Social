using Social.Dtos.Comment;
using Social.Models;

namespace Social.Mappers
{
    public static class CommentMapper
    {

        public static CommentDto ToCommentDtos(this Comment comment)
        {

            return new CommentDto()
            {
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title,
                AppUserId = "2434",
                CreatedBy=comment.AppUser.UserName 



            };
        }

        public static Comment ToCommentfromCreate(this CreateCommentDto createCommentDto, int stockId)
        {

            return new Comment()
            {
                Content = createCommentDto.Content,
                Title = createCommentDto.Title,
                AppUserId = "0000",
                StockId = stockId

            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };

        }
    }
}
