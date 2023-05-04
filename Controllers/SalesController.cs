using Microsoft.AspNetCore.Mvc;
using sales_invoicing_dotnet.Data;

namespace sales_invoicing_dotnet.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesContext _context;

        public SalesController(SalesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            if (_context.Products != null)
            {
                var productsList = _context.Products.ToList();
                return Json(productsList);
            }
            else
            {
                return Json(new List<Product>());
            }
        }

        public IActionResult GetCustomers()
        {
            if (_context.Customer != null)
            {
                var customersList = _context.Customer.ToList();
                return Json(customersList);
            }
            else
            {
                return Json(new List<Customer>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}