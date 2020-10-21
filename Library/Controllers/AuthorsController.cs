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

public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthorsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userAuthor = _db.Authors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userAuthor);
    }
    [Authorize(Policy = "RequireAdministratorRole")]
    public ActionResult Create()
    {
        ViewBag.BookId = new SelectList(_db.BookAuthors, "BookId", "Title");
        return View();
    }

    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpPost]
    public async Task<ActionResult> Create(Author author, int bookId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      author.User = currentUser;
      _db.Authors.Add(author);
      if (bookId != 0)
      {
          _db.BookAuthors.Add(new BookAuthor() { BookId = bookId, AuthorId = author.AuthorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var thisAuthor = _db.Authors
        .Include(author => author.BookAuthors)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(author => author.AuthorId == id);
        return View(thisAuthor);
    }

    [Authorize(Policy = "RequireAdministratorRole")]
    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuthor);
    }
    
    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpPost]
    public ActionResult Edit(Author author, int BookId)
    {
      if (BookId != 0)
      {
        _db.BookAuthors.Add(new BookAuthor() { BookId = BookId, AuthorId = author.AuthorId });
      }
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
        var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
        ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
        return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult AddBook(Author author, int bookId)
    {
        if (bookId != 0)
        {
        _db.BookAuthors.Add(new BookAuthor() { BookId = bookId, AuthorId = author.AuthorId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }