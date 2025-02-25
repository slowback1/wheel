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

    public async Task<FeatureResult<UserTokenResponse>> Login(string username, string password)
    {
        var user = await _dataAccess.UserRetriever.GetUser(username);

        if (user is null)
            return FeatureResult<UserTokenResponse>.Error(InvalidUsernameOrPassword);
        var storedHash = await _dataAccess.UserRetriever.GetPasswordHash(username);
        var passwordMatches = UserUtils.VerifyPassword(password, storedHash!);

        if (!passwordMatches)
            return FeatureResult<UserTokenResponse>.Error(InvalidUsernameOrPassword);

        var token = UserUtils.GenerateJWT(username);

        return FeatureResult<UserTokenResponse>.Ok(new UserTokenResponse(token));
    }
}