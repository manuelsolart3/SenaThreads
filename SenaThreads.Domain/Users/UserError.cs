using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users;
public static class UserError
{
    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid Email or password");

    public static readonly Error UserNotFound = new(
        "User.UserNotFound",
        "The specific User is not found");
    
    public static readonly Error RelationNotFound = new(
        "User.RelationNotFound",
        "Relation Not Found");
}
