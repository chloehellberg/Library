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

      public virtual ApplicationUser User { get; set; }
      public Copies Copy { get; set; }

    }
}