using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Core.Domain
{
    public class ChatRoom
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
