using Ardalis.Result;
using OrderMate.UseCases.Users;
using OrderMate.UseCases.Users.List;

namespace OrderMate.Infrastructure.Data.Queries.Users.List;

public class GetUsersQueryService(AppDbContext context) : IGetUsersQueryService
{
  public async Task<PagedResult<List<UserDto>>> GetUsersPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
  {
    var totalCount = await context.Users.CountAsync(cancellationToken);

    if (totalCount == 0)
    {
      return new PagedResult<List<UserDto>>(new PagedInfo(pageNumber, pageSize, 0, 0), []);
    }

    long totalPages = (pageSize > 0) ? (long)Math.Ceiling((double)totalCount / pageSize) : 0;

    var users = await context.Users
        .OrderBy(u => u.Name)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(u => new UserDto(u.Name, u.Email)) 
        .ToListAsync(cancellationToken);

    var pagedInfo = new PagedInfo(pageNumber, pageSize, totalPages, totalCount);

    return new PagedResult<List<UserDto>>(pagedInfo, users);
  }
}
