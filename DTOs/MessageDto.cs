using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; } // Id of the user who sent the message
        public string SenderUsername { get; set; } // Username of the user who sent the message
        public string SenderPhotoUrl { get; set; } // Photo of the user who sent the message
        public int RecipientId { get; set; } // Id of the user who received the message
        public string RecipientUsername { get; set; } // Username of the user who received the message
        public string RecipientPhotoUrl { get; set; } // Photo of the user who received the message
        public string Content { get; set; } // Content of the message
        public DateTime? DateRead { get; set; } // Date the message was read
        public DateTime MessageSent { get; set; } = DateTime.UtcNow; // Date the message was sent
    }
}