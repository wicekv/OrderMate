namespace OrderMate.Web.v1.Users.Create;

public class CreateUserResponse
{
  public CreateUserResponse(int id, string name)
  {
    Id = id;
    Name = name;
  }

  public int Id { get; set; }
  public string Name { get; set; }
}
