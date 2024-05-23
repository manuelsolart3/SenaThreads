using System.ComponentModel.DataAnnotations;
using System.Globalization;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users
{
    public class UserBlock : Entity
    {
        
        public Guid Id { get; set; }
        public User BlockedUser { get; set; }
        public string BlockedUserId { get; set; }
        public User BlockByUser { get; set; }
        public string BlockByUserId { get; set; }

        
       
        
        

        //Enum
        public BlockSatus BlockSatus { get; set; }

        public UserBlock(string blockedUserId, string blockByUserId, BlockSatus blockstatus) 
        {
            BlockedUserId = blockedUserId;
            BlockByUserId = blockByUserId;
            BlockSatus = blockstatus;
            Id = Guid.NewGuid();    
           
            

        }

       
    }
}
