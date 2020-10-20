using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Librarian> Librarians { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        optionsBuilder.UseLazyLoadingProxies();
      }
  }
}