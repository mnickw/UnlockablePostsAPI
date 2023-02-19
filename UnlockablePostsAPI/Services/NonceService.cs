using UnlockablePostsAPI.Data;

namespace UnlockablePostsAPI.Services
{
    public class NonceService : INonceService
    {
        private readonly UnlockablePostsContext _db;

        public NonceService(UnlockablePostsContext db)
        {
            _db = db;
        }

        public async Task<Guid> CreateNonce(long vk_user_id)
        {
            var guid = Guid.NewGuid();
            DateTime now = DateTime.UtcNow;

            await _db.Nonces.AddAsync(new Models.Nonce { VkUserId = vk_user_id, NonceValue = guid, CreationTime = now });
            await _db.SaveChangesAsync();

            return guid;
        }
    }
}
