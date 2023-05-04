using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using sales_invoicing_dotnet.Data;

namespace sales_invoicing_dotnet.Controllers
{
    [Produces("application/json")]
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

        [HttpPost]
        public IActionResult Create()
        {
            using (var bodyReader = new StreamReader(HttpContext.Request.Body))
            {
                var requestBody = bodyReader.ReadToEnd();
                var jsonDocument = JsonDocument.Parse(requestBody);
                var productsElement = jsonDocument.RootElement.GetProperty("products");
                var products = JsonSerializer.Deserialize<List<Product>>(productsElement.GetRawText());
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}");
               }

            }
            Console.WriteLine(Request.Form["customerId"]);
            return Ok();
        }
    }
}