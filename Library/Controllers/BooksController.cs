using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

  
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userBooks);
    }
   
   [Authorize(Policy = "RequireAdministratorRole")]
    public ActionResult Create()
    {
        ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
        return View();
    }

    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpPost]
    public async Task<ActionResult> Create(Book book, int AuthorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      book.User = currentUser;
      _db.Books.Add(book);
      if (AuthorId != 0)
      {
          _db.BookAuthors.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var thisBook = _db.Books
        .Include(book => book.BookAuthors)
        .ThenInclude(join => join.Author)
        .Include(book => book.Copies)
        // .ThenInclude(join => join.Copies)
        .FirstOrDefault(book => book.BookId == id);
        return View(thisBook);
    }

    [Authorize(Policy = "RequireAdministratorRole")]
    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }
    
    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.BookAuthors.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
        return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
        if (AuthorId != 0)
        {
        _db.BookAuthors.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
        var joinEntry = _db.BookAuthors.FirstOrDefault(entry => entry.BookAuthorId == joinId);
        _db.BookAuthors.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult AddCopy(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
        return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddCopy(Copies copies)
    {
      System.Console.WriteLine(copies.BookId);
        if (copies.BookId != 0)
        {
          copies.InStock = true;
        _db.Copies.Add(copies);
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}