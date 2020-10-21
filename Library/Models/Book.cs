using System.Collections.Generic;

namespace Library.Models
{
    public class Book{

      public Book()
      {
        this.BookAuthors= new HashSet<BookAuthor>();
        this.Copies= new HashSet<Copies>();
      }
      public int BookId { get; set; }
      public string Title { get; set; } 
      public string Description { get; set; }      
      public virtual ApplicationUser User { get; set; }
      public virtual ICollection<BookAuthor> BookAuthors { get; set;}
      public virtual ICollection<Copies> Copies { get; set;}

    }
}