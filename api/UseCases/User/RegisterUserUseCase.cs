using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.User;

public class RegisterUserUseCase : DataAccessorUseCase
{
    public RegisterUserUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<UserTokenResponse>> Register(CreateUser user)
    {
        var inputValidation = await ValidateInput(user);
        if (inputValidation != null)
            return inputValidation;

        user.Password = UserUtils.HashPassword(user.Password);

        var createdUser = await _dataAccess.UserCreator.CreateUser(user);

        var token = UserUtils.GenerateJWT(user.Username);

        return FeatureResult<UserTokenResponse>.Ok(new UserTokenResponse(token));
    }

    private async Task<FeatureResult<UserTokenResponse>?> ValidateInput(CreateUser user)
    {
        var passwordValidation = ValidatePassword(user.Password);
        if (passwordValidation != null)
            return passwordValidation;

        var userValidation = ValidateUsername(user.Username);
        if (userValidation != null)
            return userValidation;

        var storedUser = await _dataAccess.UserRetriever.GetUser(user.Username);

        if (storedUser != null)
            return FeatureResult<UserTokenResponse>.Error($"User with the name {user.Username} already exists.");

        return null;
    }

    private FeatureResult<UserTokenResponse>? ValidatePassword(string password)
    {
        if (password.Length < 8)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it is too short. It must be at least 8 characters long.");

        var passwordDoesNotHaveAnyLetters = password.All(c => char.IsDigit(c) || char.IsPunctuation(c));
        if (passwordDoesNotHaveAnyLetters)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it does not contain any letters.");

        var passwordContainsNumbers = password.Any(char.IsDigit);
        if (!passwordContainsNumbers)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it does not contain any numbers.");

        var passwordContainsSpecialCharacters = password.Any(char.IsPunctuation);
        if (!passwordContainsSpecialCharacters)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it does not contain any special characters.");

        var passwordContainsUppercase = password.Any(char.IsUpper);
        if (!passwordContainsUppercase)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it does not contain any uppercase characters.");

        var passwordContainsLowercase = password.Any(char.IsLower);
        if (!passwordContainsLowercase)
            return FeatureResult<UserTokenResponse>.Error(
                "Password has an error because it does not contain any lowercase characters.");

        return null;
    }

    private FeatureResult<UserTokenResponse>? ValidateUsername(string username)
    {
        if (username.Length < 3)
            return FeatureResult<UserTokenResponse>.Error(
                "Username has an error because it is too short. It must be at least 3 characters long.");

        return null;
    }
}