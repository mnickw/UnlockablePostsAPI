using Microsoft.Extensions.Primitives;

namespace UnlockablePostsAPI.Services
{
    public interface IUsersService
    {
        bool ValidateSignatureFromQueryString(IQueryCollection queryParams);
    }
}
