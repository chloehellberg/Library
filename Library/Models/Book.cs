namespace Library.Models
{
    public class Book{

      public Book()
      {
        this.BookAuthor= new HashSet<BookAuthors>();
        this.BookCheckout= new HashSet<BookCheckout>();
      }
      public int BookId { get; set; }
      public int Copies { get; set; }
      public string Title { get; set; }
      public string Genre { get; set; }

      public virtual ICollection<PatientDoctor> BookAuthors { get; set;}
      public virtual ICollection<SpecialtyDoctor> BookCheckout { get; set;}

    }
}