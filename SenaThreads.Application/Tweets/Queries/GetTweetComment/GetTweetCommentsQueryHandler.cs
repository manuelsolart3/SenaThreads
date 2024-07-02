﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.Tweets.Queries.GetTweetComments;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using static System.Net.Mime.MediaTypeNames;

namespace SenaThreads.Application.Tweets.Queries.GetTweetComment;
public class GetTweetCommentsQueryHandler : IQueryHandler<GetTweetCommentsQuery, Pageable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetTweetCommentsQueryHandler(IMapper mapper, ICommentRepository commentRepository, IAwsS3Service awsS3Service)
    {

        _mapper = mapper;
        _commentRepository = commentRepository;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<Pageable<CommentDto>>> Handle(GetTweetCommentsQuery request, CancellationToken cancellationToken)
    {
        var paginatedComments = await FetchData(request.TweetId, request.Page, request.PageSize);
       
        foreach (var comment in paginatedComments.List)
        {
            if (!string.IsNullOrEmpty(comment.ProfilePictureS3Key))
            {
                comment.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(comment.ProfilePictureS3Key);
            }
        }
        return Result.Success(paginatedComments);
    }

    private async Task<Pageable<CommentDto>> FetchData(Guid tweetId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<Comment> commentsQuery = _commentRepository.Queryable()
            .Where(c => c.TweetId == tweetId) 
            .Include(c => c.User)                     
            .OrderByDescending(c => c.CreatedOnUtc); // Ordenar los comentarios por fecha de creación

        int totalCount = await commentsQuery.CountAsync();//total de comentarios

        List<Comment> pagedComments = await commentsQuery
            .Skip(start)        
            .Take(pageSize)     
            .ToListAsync();     

        // Mapear los comentarios paginados a DTOs
        List<CommentDto> commentDtos = _mapper.Map<List<CommentDto>>(pagedComments);

        // Retornar nuevo Objeto pageable con la lista de Dtos y el total de comentarios
        return new Pageable<CommentDto>
        {
            List = commentDtos,
            Count = totalCount
        };
    }
}

