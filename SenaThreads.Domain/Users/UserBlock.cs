using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users;

public class UserBlock : Entity
{
    public Guid Id { get; private set; }
    public User BlockedUser { get; private set; }
    public string BlockedUserId { get; private set; }
    public User BlockByUser { get; private set; }
    public string BlockByUserId { get; private set; }

    //Enum
    public BlockSatus BlockSatus { get; private set; }

    public UserBlock(string blockedUserId, string blockByUserId, BlockSatus blockstatus) 
    {
        Id = Guid.NewGuid();
        BlockedUserId = blockedUserId;
        BlockByUserId = blockByUserId;
        BlockSatus = blockstatus;
    }
    public UserBlock() { }
}
