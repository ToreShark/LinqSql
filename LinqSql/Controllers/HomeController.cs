using System.Diagnostics;
using LinqSql.Data;
using Microsoft.AspNetCore.Mvc;
using LinqSql.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqSql.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationContext db)
    {
        _logger = logger;
        _db = db;
        _db.Database.EnsureDeleted();
        _db.Database.EnsureCreated();
        Company microsoft = new Company { Name = "Microsoft" };
        Company google = new Company { Name = "Google" };
        _db.Companies.AddRange(microsoft, google);

        User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
        User bob = new User { Name = "Bob", Age = 39, Company = google };
        User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
        User kate = new User { Name = "Kate", Age = 25, Company = google };

        _db.User.AddRange(tom, bob, alice, kate);
        _db.SaveChanges();
    }

    public IActionResult Index()
    {
        string stringText = "";
        
        var testUser = (from user in _db.User select  user).ToList();
        var users = (from user in _db.User.Include(p => p.Company)
            where user.CompanyId == 1
            select user).ToList();

        foreach (var user in users)
            stringText += $"{user.Name} ({user.Age}) - {user.Company?.Name}";

        return View();

        return View();
    }

    public string Index2()
    {
        string stringResult = "";
        bool isAvailable = _db.Database.CanConnect();
        if (isAvailable)
        {
            User tom = new User { Name = "Tom", Age = 23 };
            User bob = new User { Name = "Bob", Age = 34 };
            _db.User.Add(tom);
            _db.User.Add(bob);
            _db.SaveChanges();
            var users = _db.User.ToList();
            foreach (User u in users)
            {
                stringResult += $"{u.UserId}.{u.Name} - {u.Age} /n";
            }
        }

        return stringResult;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}