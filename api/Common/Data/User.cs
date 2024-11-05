namespace Common.Data;

public class User
{
    public string Username { get; set; }
}

public class CreateUser
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUser
{
    public string Username { get; set; }
    public string Password { get; set; }
}