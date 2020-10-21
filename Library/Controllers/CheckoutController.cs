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
using System;

namespace Library.Controllers
{
  [Authorize]
  public class CheckoutController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public CheckoutController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    
    public async Task<ActionResult> Index() 
    {
      // System.Console.WriteLine("test"); 
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userCheckouts = _db.Checkouts.Where(entry => entry.User.Id == currentUser.Id)
        .Include(x => x.Copy)
        .ThenInclude(x => x.Book)
        .ToList();
      
      return View(userCheckouts);
    }
    // public async Task<ActionResult> CheckOutBooks() 
    // {
    //   // System.Console.WriteLine("test"); 
    //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(userId);
    //   var userCheckouts = _db.Checkouts.Where(entry => entry.PatronId == currentUser.Id).ToList();
      
    //   return View(userCheckouts);
    // }
    
    public async Task<ActionResult> CheckOutBook(int id) //id book
    {
      // System.Console.WriteLine("test"); 
      
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      // var userCheckouts = _db.Checkouts.Where(entry => entry.PatronId == currentUser.Id).ToList();
      var booksInStock = _db.Copies.Where(x => x.BookId == id).Where(x => x.InStock == true).ToList();
      if(booksInStock.Count != 0)
      {
        Checkout newCheckout = new Checkout(){CopiesId = booksInStock[0].CopiesId, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) };
        booksInStock[0].InStock = false;
        newCheckout.User = currentUser;
        _db.Checkouts.Add(newCheckout);
        _db.SaveChanges();
      }
      
      
      return RedirectToAction("Index");
    }
  }
}