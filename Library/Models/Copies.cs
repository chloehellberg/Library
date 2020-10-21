using System.Collections.Generic;

namespace Library.Models
{
  public class Copies
  {
    public Copies()
    {
      this.Checkout = new HashSet<Checkout>();
    }
    public int CopiesId { get; set; }
    public bool InStock { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public virtual ICollection<Checkout> Checkout { get; set; }
  }
}