namespace UnlockablePostsAPI.Services
{
    public interface INonceService
    {
        public Task<Guid> CreateNonce(long vk_user_id);
        public Task<bool> ValidateNonce(long vk_user_id, string signedNonce, string newAddress);
    }
}
