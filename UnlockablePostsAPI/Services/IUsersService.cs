using Microsoft.Extensions.Primitives;

namespace UnlockablePostsAPI.Services
{
    public interface IUsersService
    {
        bool ValidateSignatureFromQueryString(IQueryCollection queryParams);
        Task<long?> CheckExistingUserForAddress(string address);
        Task AddAddressForUser(long vk_user_id, string address);
    }
}
