using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnlockablePostsAPI.Models
{
    public class Nonce
    {
        public int Id { get; set; }

        [Required]
        public long VkUserId { get; set; }

        [Required]
        public Guid NonceValue { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
    }
}
