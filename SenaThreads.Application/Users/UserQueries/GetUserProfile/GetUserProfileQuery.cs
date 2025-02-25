﻿using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserProfile;
public record GetUserProfileQuery(string userId) : IQuery<UserProfileDto>;
