using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateMessageDto
    {
        public string RecipientUsername { get; set; } // Username of the user who received the message
        public string Content { get; set; } // Content of the message
    }
}