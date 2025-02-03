using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
