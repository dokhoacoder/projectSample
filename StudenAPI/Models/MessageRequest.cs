using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudenAPI.Models
{
    public class MessageRequest
    {
        public string status { get; set; }
        public string message { get; set; }
        public string description { get; set; }
    }
}