namespace UnlockablePostsAPI.Services
{
    public interface INonceService
    {
        public Task<Guid> CreateNonce(long vk_user_id);
    }
}
