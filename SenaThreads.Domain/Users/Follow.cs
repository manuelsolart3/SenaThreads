using System.ComponentModel.DataAnnotations;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users
{
    public class Follow : Entity

    {
       
        
        public string FollowerUserId { get; private set; }
        public User FollowerUser { get; private set; }
        public string FollowedByUserId { get; private set; }
        public User FollowedByUser { get; private set; }


        public Follow(string followerUserId, string followedUserId) 
        {

            FollowedByUserId = followedUserId;
            FollowerUserId = followerUserId;
            
        
        }
       
    }

    
}
