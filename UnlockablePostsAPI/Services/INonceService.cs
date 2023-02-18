namespace UnlockablePostsAPI.Services
{
    public interface INonceService
    {
        public Guid CreateNonce(int vk_user_id);
    }
}
