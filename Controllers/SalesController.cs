using Microsoft.AspNetCore.Mvc;

public class SalesController : Controller
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