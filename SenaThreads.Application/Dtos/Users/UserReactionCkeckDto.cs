namespace SenaThreads.Application.Dtos.Users;
public  class UserReactionCkeckDto
{
    public bool HasReacted  { get; set; }
    public int ReactionType  { get; set; }
    public string Message  { get; set; }
}
