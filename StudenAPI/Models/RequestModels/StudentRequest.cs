using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudenAPI.Models
{
    public class StudentRequest
    {
        public int totalPage { get; set; }
        public int Page { get; set; }
        public List<Sinhvien> data { get; set; }
    }
}