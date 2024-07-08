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

    public static readonly Error ErrorupdateResult = new(
        "User.ErrorupdateResult",
        "Error updating information");

    public static readonly Error AlreadyExists = new(
        "User.AlreadyExists",
        "This action already exist");

    public static readonly Error RegistrationFailed = new(
        "User.RegistrationFailed",
        "Failed to register use");

    public static readonly Error Unauthorized = new(
        "User.Unauthorized",
        "The user is not Unauthorized");

    public static readonly Error PasswordResetFailed = new(
        "User.PasswordResetFailed",
        "error occurred while resetting the password");
    
    public static readonly Error EmailAlreadyExists = new(
        "User.EmailAlreadyExists",
        "The Specific Email already exist");


     public static readonly Error UsernameAlreadyExists = new(
        "User.UsernameAlreadyExists",
        "The Specific username already exist"); 
    
    public static readonly Error InvalidToken = new(
        "User.InvalidToken",
        "The Specific Token is not correct");

    public static readonly Error CannotOwnInformation = new(
        "User.CannotOwnInformation",
        "you Cannot visit own profile");

}
