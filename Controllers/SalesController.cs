using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (_context.Customers != null)
            {
                var customersList = _context.Customers.ToList();
                return Json(customersList);
            }
            else
            {
                return Json(new List<Customer>());
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create()
        {
            try
            {
                using (var bodyReader = new StreamReader(HttpContext.Request.Body))
                {
                    var requestBody = await bodyReader.ReadToEndAsync();
                    var jsonDocument = JsonDocument.Parse(requestBody);

                    var customerIdStr = jsonDocument.RootElement.GetProperty("customerId").GetString();
                    int.TryParse(customerIdStr, out int customerId);

                    var dateStr = jsonDocument.RootElement.GetProperty("date").GetString();
                    DateTime.TryParse(dateStr, out DateTime date);

                    var productsElement = jsonDocument.RootElement.GetProperty("products");
                    var soldProducts = JsonSerializer.Deserialize<List<SoldProduct>>(productsElement.GetRawText());

                    if (soldProducts == null || soldProducts.Count == 0)
                    {
                        return BadRequest("First add products to this sales order");
                    }

                    var taxValueStr = jsonDocument.RootElement.GetProperty("taxValue").GetString();
                    decimal.TryParse(taxValueStr, out decimal taxValue);

                    var taxPercentageStr = jsonDocument.RootElement.GetProperty("taxPercentage").GetString();
                    int.TryParse(taxPercentageStr, out int taxPercentage);

                    var discountValueStr = jsonDocument.RootElement.GetProperty("discountValue").GetString();
                    decimal.TryParse(discountValueStr, out decimal discountValue);

                    var discountPercentageStr = jsonDocument.RootElement.GetProperty("discountPercentage").GetString();
                    int.TryParse(discountPercentageStr, out int discountPercentage);

                    var shippingStr = jsonDocument.RootElement.GetProperty("shipping").GetString();
                    decimal.TryParse(shippingStr, out decimal shipping);

                    var grandTotalStr = jsonDocument.RootElement.GetProperty("grandTotal").GetString();
                    decimal.TryParse(grandTotalStr, out decimal grandTotal);

                    // Get the customer from the database
                    var customer = await _context.Customers.FindAsync(customerId);

                    if (customer == null)
                    {
                        return BadRequest("Customer not found");
                    }

                    // Create a new invoice and assign the customer to it
                    var invoice = new Invoice
                    {
                        Date = date,
                        Customer = customer,
                        TaxPercent = taxPercentage,
                        TaxValue = taxValue,
                        DiscountPercent = discountPercentage,
                        DiscountValue = discountValue,
                        Shipping = shipping,
                        GrandTotal = grandTotal,
                        SoldProducts = new List<SoldProduct>()
                    };

                    // Add sold products to the invoice
                    foreach (var soldProduct in soldProducts)
                    {
                        // Get product from DB
                        var product = await _context.Products.FindAsync(soldProduct.ProductId);

                        // Add sold product to invoice
                        invoice.SoldProducts.Add(new SoldProduct
                        {
                            Product = product,
                            Quantity = soldProduct.Quantity
                        });
                    }

                    // save the invoice
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred: {ex.Message}\nStack trace: {ex.StackTrace}\nLine number: {ex.LineNumber()}";
                return BadRequest(errorMessage);
            }
        }

        public async Task<IActionResult> Invoices()
        {
            var invoices = await _context.Invoices.Include(i => i.Customer).ToListAsync();
            return View(invoices);
        }

        public IActionResult ViewInvoice(int id)
        {
            var invoice = _context.Invoices.Include(i => i.Customer).Include(i => i.SoldProducts).ThenInclude(sp => sp.Product).FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

    }
}