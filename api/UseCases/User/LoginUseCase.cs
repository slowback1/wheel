using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.User;

public class LoginUseCase : DataAccessorUseCase
{
    private const string InvalidUsernameOrPassword = "Invalid username or password.";

    public LoginUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<string>> Login(string username, string password)
    {
        var user = await _dataAccess.UserRetriever.GetUser(username);

        if (user is null)
            return FeatureResult<string>.Error(InvalidUsernameOrPassword);
        var storedHash = await _dataAccess.UserRetriever.GetPasswordHash(username);
        var passwordMatches = PasswordUtils.VerifyPassword(password, storedHash!);

        if (!passwordMatches)
            return FeatureResult<string>.Error(InvalidUsernameOrPassword);

        return FeatureResult<string>.Ok("hash");
    }
}