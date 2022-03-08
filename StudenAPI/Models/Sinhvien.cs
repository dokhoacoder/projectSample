using System;
using System.Collections.Generic;

#nullable disable

namespace StudenAPI.Models
{
    public partial class Sinhvien
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public string Cmnd { get; set; }
        public string Phone { get; set; }
        public string Class { get; set; }
    }
}