using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppPélda.Models;
using WebAppPélda.Services;

namespace WebAppPelda.Controllers
{
    public class HomeController : Controller
    {
        List<Customer> customers = new List<Customer> {new Customer
        {
            Id = 1,
            Nev = "Név",
            Telefon = "00112223344",
            Pontszam = 5
        }, new Customer
        {
            Id = 2,
            Nev = "Másik név",
            Telefon = "000-111-2223",
            Pontszam = 85
        } };
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Vasarlo(int Id)
        {
           Customer customer = customers.First(c => c.Id == Id);
            return View(customer);
        }
        public IActionResult Sajat()
        {
            List<Customer> customers = new VasarloService().GetAllCustomer();
            return View(customers);
        }
        public IActionResult VasarloAdatai(int id)
        {
            Customer valasztottVasarlo = new VasarloService().GetById(id);
            return View(valasztottVasarlo);
        }
        [HttpGet]
        public IActionResult CreateVasarlo()
        {
            Customer uresCustomer = new Customer();
            return View(uresCustomer);
        }
        [HttpPost]
        public IActionResult CreateVasarlo(Customer customer)
        {
            string result = new VasarloService().POSTCustomer(customer);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(CreateVasarlo));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}