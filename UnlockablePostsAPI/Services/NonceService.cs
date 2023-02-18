namespace UnlockablePostsAPI.Services
{
    public class NonceService : INonceService
    {
        public Guid CreateNonce(int vk_user_id)
        {
            var guid = Guid.NewGuid();



            return guid;
        }
    }
}
