using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MessageParams : PaginationParams
    {
        public string Username { get; set; } // Username of the user who sent the message
        public string Container { get; set; } = "Unread"; // Container of the message
    }
}