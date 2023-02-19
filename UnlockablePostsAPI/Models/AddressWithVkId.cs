using System.ComponentModel.DataAnnotations;

namespace UnlockablePostsAPI.Models
{
    public class AddressWithVkId
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public long VkUserId { get; set; }
    }
}
