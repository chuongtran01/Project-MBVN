using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Patient model = new()
        {
            Firstname = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam cursus orci urna, eget ornare turpis mattis sed. Aenean vitae consectetur libero, nec congue felis. Integer id velit dictum, suscipit nisl quis, malesuada turpis. Nulla facilisi. Phasellus blandit placerat purus ac malesuada. Sed convallis dolor id turpis iaculis rhoncus. Praesent posuere augue interdum est hendrerit elementum. Suspendisse dapibus nisi eros, sed porttitor dolor vestibulum et. Aliquam id varius metus.\r\n\r\nEtiam convallis orci ligula. Donec urna eros, tincidunt ullamcorper aliquam ac, iaculis et metus. Donec at justo ac ante sagittis feugiat ut sed est. Proin pellentesque dignissim turpis vel sollicitudin. Praesent eu aliquet purus, nec consequat enim. In feugiat orci id enim dictum volutpat a in velit. Aliquam ultricies pulvinar elit, ac interdum sem tristique id.\r\n\r\nSed sed ante vulputate metus consectetur rhoncus sed quis tellus. In hendrerit pulvinar placerat. Integer id arcu vitae sem congue aliquet eu vitae arcu. Sed scelerisque ligula risus, quis semper sapien ultrices in. Suspendisse potenti. Etiam id tempus leo, ac cursus diam. Curabitur blandit lobortis ligula. Duis sapien elit, blandit ac ullamcorper vel, aliquet eget massa."
        };
        return View(model);
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

