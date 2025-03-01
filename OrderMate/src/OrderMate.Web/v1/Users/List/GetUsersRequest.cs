namespace OrderMate.Web.v1.Users.List;

public sealed record GetUsersRequest(int PageNumber, int PageSize)
{
  public const string Route = "/api/users";

  public static string BuildRoute(int pageNumber, int pageSize) =>
      $"{Route}?pageNumber={pageNumber}&pageSize={pageSize}";
}
