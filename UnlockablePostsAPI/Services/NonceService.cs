using Microsoft.EntityFrameworkCore;
using Nethereum.Signer;
using UnlockablePostsAPI.Data;

namespace UnlockablePostsAPI.Services
{
    public class NonceService : INonceService
    {
        private readonly UnlockablePostsContext _db;
        private readonly EthereumMessageSigner _signer;

        public NonceService(UnlockablePostsContext db,
            EthereumMessageSigner signer)
        {
            _db = db;
            _signer = signer;
        }

        public async Task<Guid> CreateNonce(long vk_user_id)
        {
            var guid = Guid.NewGuid();
            DateTime now = DateTime.UtcNow;

            await _db.Nonces.AddAsync(new Models.Nonce { VkUserId = vk_user_id, NonceValue = guid, CreationTime = now });
            await _db.SaveChangesAsync();

            return guid;
        }

        public async Task<bool> ValidateNonce(long vk_user_id, string signedNonce, string newAddress)
        {
            var nonces = await _db.Nonces.Where(n => n.VkUserId == vk_user_id).ToListAsync();

            foreach (var nonce in nonces)
            {
                var addressRecovered = _signer.EncodeUTF8AndEcRecover(nonce.NonceValue.ToString(), signedNonce);

                if (addressRecovered == newAddress)
                    return true;
            }

            return false;
        }
    }
}
