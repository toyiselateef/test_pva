using System.Resources;

public static class UserMessages
{
    private static readonly ResourceManager _resourceManager = new ResourceManager("Tamada.Middleware.Domain.UserMessages", typeof(UserMessages).Assembly);

    public static string GetAccountNotValidUserFriendlyMessage()
    {
        return _resourceManager.GetString("AccountNotValid");
    }
    public static string GetAccountNoBadRequestUserFriendlyMessage()
    {
        return _resourceManager.GetString("AccNoBadRequest");
    }

}