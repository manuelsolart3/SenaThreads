namespace SenaThreads.Application.Dtos.Users;
public class UserSearchHistoryDto
{
    public int Id { get; set; }
    public string SearchedUserId { get; set; }
    public DateTime SearchedAt { get; set; }
}
