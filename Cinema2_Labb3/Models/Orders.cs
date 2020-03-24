using System;
using System.Collections.Generic;

namespace Cinema_Labb3.Models
{
    public partial class Orders
    {


        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvc { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? TotalPrice { get; set; }
        public string Movie { get; set; }

    }
}
