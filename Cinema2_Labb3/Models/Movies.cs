using System;
using System.Collections.Generic;

namespace Cinema_Labb3.Models
{
    public partial class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int? Price { get; set; }
        public Guid? Order { get; set; }
        public string Time { get; set; }
        public string Salon { get; set; }

    }
}
