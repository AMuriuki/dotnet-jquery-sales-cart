using Microsoft.AspNetCore.Mvc;

public class InvoiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
}