namespace OrderMate.Core.Aggregates.UserAggregate.ErrorMessages;

public static class UserErrors
{
  public const string UserUniq = "User name must be unique.";
  public const string InvalidEmail = "Invalid email format.";
  public const string NameRequired = "User name is required.";
  public const string UserNotFound = "User does not exist.";

}
