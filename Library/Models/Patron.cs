using System;
using System.Collections.Generic;
namespace Library.Models
{
    public class Patron
    {
      public int PatronId { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public virtual ApplicationUser User { get; set; }
      public ICollection<Checkout> Checkouts { get; set; }
    }
}