using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Post;
using SenaThreads.Domain.Users;

namespace SenaThreads.Domain.Tweets;

public class Tweet : Entity
{
    
    public Guid Id { get; private set; }
    public User User { get; private set; }
    public string UserId { get; private set; }
    public string Text { get; private set; }
    
    // Propiedades de navegación
    public List<TweetAttachment> Attachments { get; set; }
    public ICollection<Comment> Comments { get; set; } // Comentarios asociados a este tweet
    public ICollection<Retweet> Retweets { get; set; } // Retweets asociados a este tweet
    public ICollection<Reaction> Reactions { get; set; } //Reacciones asociadas a este tweet

    public Tweet(string userId, string text)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Text = text;
    }
}
