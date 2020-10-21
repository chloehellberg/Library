using System;
using System.Collections.Generic;
namespace Library.Models
{
    public class Checkout
    {
      public int CheckoutId { get; set; }
      public DateTime DueDate { get; set; }
      public DateTime CheckoutDate { get; set; }
      public int CopiesId { get; set; }
      public int PatronId { get; set; }

      public Patron Patron { get; set; } 
      public Copies Copy { get; set; }

    }
}