﻿using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Tweets;

public class TweetAttachment : Entity
{
    public Guid Id { get; private set; }
    public Tweet Tweet { get; private set; }
    public Guid TweetId { get; private set; }
    public string Key { get; private set; }

    public TweetAttachment(Tweet tweet, string key)
    {
        Id = Guid.NewGuid();
        Tweet = tweet;
        Key = key;
    }
}
