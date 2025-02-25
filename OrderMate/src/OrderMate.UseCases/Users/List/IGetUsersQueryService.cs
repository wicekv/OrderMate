namespace OrderMate.UseCases.Users.List;
public interface IGetUsersQueryService
{
  Task<PagedResult<List<UserDto>>> GetUsersPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
