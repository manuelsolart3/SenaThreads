namespace SenaThreads.Application.Dtos.Users;
public class FollowerResultDto
{
    public List<FollowerDto> Followers { get; set; }
    public int TotalCount { get; set; }

    public FollowerResultDto(List<FollowerDto> followers, int totalCount)
    {
        Followers = followers;
        TotalCount = totalCount;
    }
}
