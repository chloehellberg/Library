using System.Collections.Generic;

namespace Library.Models
{
    public class Book{

      public Book()
      {
        this.BookAuthors= new HashSet<BookAuthor>();
      }
      public int BookId { get; set; }
      public int Copies { get; set; }
      public string Title { get; set; }
      public string Genre { get; set; }
      public virtual ApplicationUser User { get; set; }
      public virtual ICollection<BookAuthor> BookAuthors { get; set;}

    }
}