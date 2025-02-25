namespace Common.Data;

public class UserTokenResponse
{
    public UserTokenResponse(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}