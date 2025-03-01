namespace OrderMate.Web.v1.Users.Orders.List;

public sealed class GetUserOrdersRequest
{

  public const string Route = "/api/users/{userId}/orders";

  public static string BuildRoute(int userId) => Route.Replace("{userId}", userId.ToString());

  public int UserId { get; init; }

}
