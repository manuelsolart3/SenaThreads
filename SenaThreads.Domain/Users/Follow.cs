using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users;

public class Follow : Entity
{
    public Guid Id { get; set; }
    public User FollowerUser { get; private set; }  //Usario seguidor
    public string FollowerUserId { get; private set; }
    public User FollowedByUser { get; private set; } //Usuario seguido
    public string FollowedByUserId { get; private set; }


    public Follow(string followerUserId, string followedUserId) 
    {
        Id = Guid.NewGuid();
        FollowedByUserId = followedUserId;
        FollowerUserId = followerUserId;
        CreatedBy = followerUserId;
        UpdatedBy = followerUserId;
    }
    public Follow ()
    {

    }
}


